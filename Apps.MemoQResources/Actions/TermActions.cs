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

namespace Apps.MemoQResources.Actions;

[ActionList]
public class TermActions : BaseInvocable
{
    public TermActions(InvocationContext invocationContext) : base(invocationContext)
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