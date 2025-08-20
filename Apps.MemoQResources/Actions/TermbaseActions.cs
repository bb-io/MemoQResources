using System.Text.Json.Serialization;
using System.Text.Json;
using Apps.MemoQResources.Api;
using Apps.MemoQResources.Invocables;
using Apps.MemoQResources.Models.Items;
using Apps.MemoQResources.Models.Request;
using Apps.MemoQResources.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System.ComponentModel;

namespace Apps.MemoQResources.Actions;

[ActionList]
public class TermbaseActions : BaseInvocable
{
    public TermbaseActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Update term", Description = "Updates a termbase entry")]
    public async Task<UpdateTermResponse> UpdateTerm([ActionParameter] UpdateTermRequest input)
    {
            var client = new MemoQResourcesClient(InvocationContext.AuthenticationCredentialsProviders);
            var getRequest = new RestRequest($"/tbs/{input.Guid}/entries/{input.EntryId}", Method.Get);

            var existingEntry = await client.ExecuteWithErrorHandling<TermbaseEntryResponse>(getRequest);
            if (existingEntry == null)
                throw new PluginApplicationException("Failed to fetch the existing termbase entry.");

            var username = InvocationContext.AuthenticationCredentialsProviders
                .FirstOrDefault(p => p.KeyName == "username")?.Value;

            var updateDictionary = BuildUpdateDictionary(existingEntry, input, username);

            var cleanedDictionary = RemoveNullValues(updateDictionary);

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never
            };
            var serializedBody = JsonSerializer.Serialize(cleanedDictionary, jsonOptions);

            var updateRequest = new RestRequest(
                $"/tbs/{input.Guid}/entries/{input.EntryId}/update", Method.Post);
            updateRequest.AddStringBody(serializedBody, ContentType.Json);

            var updateResponse = await client.ExecuteWithErrorHandling(updateRequest);
            if (!updateResponse.IsSuccessful)
                throw new PluginApplicationException($"Error updating term: {updateResponse.Content}");

            var getUpdatedRequest = new RestRequest($"/tbs/{input.Guid}/entries/{input.EntryId}", Method.Get);
            var updatedEntry = await client.ExecuteWithErrorHandling<TermbaseEntryResponse>(getUpdatedRequest);
            if (updatedEntry == null)
                throw new PluginApplicationException("Failed to fetch the updated entry.");

            return new UpdateTermResponse
            {
                UpdatedEntry = updatedEntry
            };
    }

    [Action("Search termbases", Description = "Gets termbases from memoQ resource server.")]
    public async Task<ListTermBaseResponse> SearchTermbases([ActionParameter] ListTermBaseRequest input)
    {
        var client = new MemoQResourcesClient(InvocationContext.AuthenticationCredentialsProviders);

        var request = new RestRequest("/tbs", Method.Get);

        if (input.Languages != null)
        {
            var langs = input.Languages.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            for (int i = 0; i < langs.Length; i++)
                request.AddQueryParameter($"lang[{i}]", langs[i]);
        }

        var list = await client.ExecuteWithErrorHandling<List<TermBaseListItem>>(request)
                   ?? throw new PluginApplicationException("Failed to fetch termbases.");

        if (!string.IsNullOrWhiteSpace(input.ExactName))
        {
            list = list
                .Where(x => string.Equals(x.FriendlyName, input.ExactName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        else if (!string.IsNullOrWhiteSpace(input.NameContains))
        {
            list = list
                .Where(x => (x.FriendlyName ?? string.Empty)
                    .Contains(input.NameContains, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return new ListTermBaseResponse { Termbases = list };
    }

    [Action("Create term", Description = "Creates a termbase entry in the specified TB")]
    public async Task<CreateTermResponse> CreateTerm(
    [ActionParameter] CreateTermbaseEntryRequest input)
    {
        if (string.IsNullOrWhiteSpace(input.Guid))
            throw new PluginMisconfigurationException("Termbase ID is required.");
        if (string.IsNullOrWhiteSpace(input.Language))
            throw new PluginMisconfigurationException("Language is required.");
        if (string.IsNullOrWhiteSpace(input.Text))
            throw new PluginMisconfigurationException("Text is required.");

        var client = new MemoQResourcesClient(InvocationContext.AuthenticationCredentialsProviders);

        int? caseSense = ToNullableInt(input.CaseSense, "Case sensitivity");
        int? partialMatch = ToNullableInt(input.PartialMatch, "Partial match");

        var languages = new List<Dictionary<string, object?>>
    {
        new()
        {
            ["Language"] = input.Language,
            ["Definition"] = input.Definition,
            ["NeedsModeration"] = input.NeedsModeration,
            ["TermItems"] = new List<Dictionary<string, object?>>
            {
                new()
                {
                    ["Text"] = input.Text,
                    ["Example"] = input.Example,
                    ["CaseSense"] = caseSense,
                    ["PartialMatch"] = partialMatch,
                    ["IsForbidden"] = input.IsForbidden
                }
            }
        }
    };

        var body = new Dictionary<string, object?>
        {
            ["Client"] = input.Client,
            ["Domain"] = input.Domain,
            ["Note"] = input.Note,
            ["Project"] = input.Project,
            ["Subject"] = input.Subject,
            ["Languages"] = languages
        };
        var cleaned = RemoveNullValues(body.ToDictionary(k => k.Key, v => (object?)v.Value ?? null!));

        var request = new RestRequest($"/tbs/{input.Guid}/entries/create", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddQueryParameter("returnNewEntry", "true");
        request.AddJsonBody(cleaned);

        var created = await client.ExecuteWithErrorHandling<TermbaseEntryResponse>(request);
        if (created == null)
            throw new PluginApplicationException("The API did not return the created entry.");

        return new CreateTermResponse { CreatedEntry = created };
    }

    [Action("Delete term", Description = "Deletes a termbase entry ")]
    public async Task<DeleteTermbaseEntryResponse> DeleteTermbaseEntry([ActionParameter] DeleteTermbaseEntryRequest input)
    {
        if (string.IsNullOrWhiteSpace(input.Guid))
            throw new PluginMisconfigurationException("Termbase ID is required.");
        if (string.IsNullOrEmpty(input.EntryId))
            throw new PluginMisconfigurationException("Entry ID must be a positive integer.");

        var client = new MemoQResourcesClient(InvocationContext.AuthenticationCredentialsProviders);
        var request = new RestRequest($"/tbs/{input.Guid}/entries/{input.EntryId}/delete", Method.Post);

        var resp = await client.ExecuteWithErrorHandling(request);
        if (!resp.IsSuccessful)
            throw new PluginApplicationException($"Failed to delete entry {input.EntryId} in TB {input.Guid}: {resp.Content}");

        return new DeleteTermbaseEntryResponse { Deleted = true };
    }


    private static int? ToNullableInt(string? s, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;

        if (int.TryParse(s.Trim(), out var n)) return n;
        var digits = new string(s.Where(char.IsDigit).ToArray());
        if (int.TryParse(digits, out var m)) return m;

        throw new PluginMisconfigurationException($"{fieldName} must be a number.");
    }

    private Dictionary<string, object> BuildUpdateDictionary(TermbaseEntryResponse entry,
        UpdateTermRequest input, string username)
    {
        var dict = new Dictionary<string, object>
        {
            ["Created"] = entry.Created,
            ["Creator"] = entry.Creator,
            ["Modified"] = DateTime.UtcNow,
            ["Modifier"] = string.IsNullOrWhiteSpace(username) ? entry.Modifier : username,
            ["Id"] = entry.Id
        };

        dict["Client"] = !string.IsNullOrWhiteSpace(input.Client) ? input.Client : entry.Client;
        dict["Domain"] = !string.IsNullOrWhiteSpace(input.Domain) ? input.Domain : entry.Domain;
        dict["Note"] = !string.IsNullOrWhiteSpace(input.Note) ? input.Note : entry.Note;
        dict["Project"] = !string.IsNullOrWhiteSpace(input.Project) ? input.Project : entry.Project;
        dict["Subject"] = !string.IsNullOrWhiteSpace(input.Subject) ? input.Subject : entry.Subject;

        var updatedLanguageDictionaries = new List<Dictionary<string, object>>();

        foreach (var langDto in entry.Languages ?? new List<LanguageItemDto>())
        {
            var langDict = new Dictionary<string, object>
            {
                ["Id"] = langDto.Id,
                ["Language"] = langDto.Language,
                ["NeedsModeration"] = langDto.NeedsModeration,
                ["Definition"] = langDto.Definition
            };

            if (string.Equals(langDto.Language, input.Language, StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrWhiteSpace(input.Definition))
                    langDict["Definition"] = input.Definition;

                if (input.Moderation.HasValue)
                    langDict["NeedsModeration"] = input.Moderation.Value;
            }

            var termItemDicts = new List<Dictionary<string, object>>();
            foreach (var termDto in langDto.TermItems ?? new List<TermItemDto>())
            {
                var tDict = new Dictionary<string, object>
                {
                    ["Id"] = termDto.Id,
                    ["Text"] = termDto.Text,
                    ["CaseSense"] = termDto.CaseSense,
                    ["Example"] = termDto.Example,
                    ["IsForbidden"] = termDto.IsForbidden,
                    ["PartialMatch"] = termDto.PartialMatch
                };

                if (string.Equals(langDto.Language, input.Language, StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(input.Text))
                        tDict["Text"] = input.Text;

                    if (!string.IsNullOrWhiteSpace(input.Example))
                        tDict["Example"] = input.Example;

                    if (input.CaseSense.HasValue)
                        tDict["CaseSense"] = input.CaseSense.Value;

                    if (input.TermIsForbidden.HasValue)
                        tDict["IsForbidden"] = input.TermIsForbidden.Value;

                    if (input.TermPartialMatches.HasValue)
                        tDict["PartialMatch"] = input.TermPartialMatches.Value;
                }

                termItemDicts.Add(tDict);
            }

            if (termItemDicts.Any())
                langDict["TermItems"] = termItemDicts;

            updatedLanguageDictionaries.Add(langDict);
        }
        dict["Languages"] = updatedLanguageDictionaries;

        return dict;
    }

    private Dictionary<string, object> RemoveNullValues(Dictionary<string, object> dictionary)
    {
        var cleaned = new Dictionary<string, object>();

        foreach (var kvp in dictionary)
        {
            if (kvp.Value is Dictionary<string, object> nestedDict)
            {
                var cleanedNested = RemoveNullValues(nestedDict);
                if (cleanedNested.Any())
                    cleaned[kvp.Key] = cleanedNested;
            }
            else if (kvp.Value is IEnumerable<object> list)
            {
                var cleanedList = new List<object>();
                foreach (var item in list)
                {
                    if (item == null)
                        continue;

                    if (item is Dictionary<string, object> itemDict)
                    {
                        var nested = RemoveNullValues(itemDict);
                        if (nested.Any())
                            cleanedList.Add(nested);
                    }
                    else
                    {
                        cleanedList.Add(item);
                    }
                }
                if (cleanedList.Any())
                    cleaned[kvp.Key] = cleanedList;
            }
            else
            {
                if (kvp.Value != null)
                    cleaned[kvp.Key] = kvp.Value;
            }
        }
        return cleaned;
    }
}