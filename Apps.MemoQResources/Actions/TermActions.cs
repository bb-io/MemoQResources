using Apps.MemoQResources.Api;
using Apps.MemoQResources.Invocables;
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

    [Action("Update term", Description = "Updates a termbase entry in memoQ")]
    public async Task<UpdateTermResponse> UpdateTerm([ActionParameter] UpdateTermRequest input)
    {
        try
        {
            var client = new MemoQResourcesClient(InvocationContext.AuthenticationCredentialsProviders);
            var request = new RestRequest(
                $"memoqserverhttpapi/v1/tbs/{input.Guid}/entries/{input.EntryId}/update", Method.Post);
            var username = InvocationContext.AuthenticationCredentialsProviders
                        .FirstOrDefault(p => p.KeyName == "username")?.Value;

            var languages = new List<object>();

            for (int i = 0; i < input.Languages.Count(); i++)
            {
                var termItem = new Dictionary<string, object>();

                if (input.CaseSense?.ElementAtOrDefault(i) != null)
                    termItem["CaseSense"] = input.CaseSense.ElementAtOrDefault(i);
                if (input.Example?.ElementAtOrDefault(i) != null)
                    termItem["Example"] = input.Example.ElementAtOrDefault(i);
                if (input.TermIsForbidden?.ElementAtOrDefault(i) != null)
                    termItem["IsForbidden"] = input.TermIsForbidden.ElementAtOrDefault(i);
                if (input.TermPartialMatches?.ElementAtOrDefault(i) != null)
                    termItem["PartialMatch"] = input.TermPartialMatches.ElementAtOrDefault(i);
                if (!string.IsNullOrWhiteSpace(input.Text.ElementAtOrDefault(i)))
                    termItem["Text"] = input.Text.ElementAtOrDefault(i);

                var languageItem = new Dictionary<string, object>
                {
                    ["Language"] = input.Languages.ElementAt(i)
                };

                if (input.Definition?.ElementAtOrDefault(i) != null)
                    languageItem["Definition"] = input.Definition.ElementAtOrDefault(i);
                if (input.Moderation?.ElementAtOrDefault(i) != null)
                    languageItem["NeedsModeration"] = input.Moderation.ElementAtOrDefault(i);

                if (termItem.Count > 0)
                    languageItem["TermItems"] = new List<object> { termItem };

                languages.Add(languageItem);
            }

            var bodyObject = new Dictionary<string, object>
            {
                ["Created"] = DateTime.UtcNow,
                ["Creator"] = "API",
                ["Modified"] = DateTime.UtcNow,
                ["Modifier"] = username,
                ["Languages"] = languages
            };

            if (!string.IsNullOrWhiteSpace(input.Client)) bodyObject["Client"] = input.Client;
            if (!string.IsNullOrWhiteSpace(input.Domain)) bodyObject["Domain"] = input.Domain;
            if (!string.IsNullOrWhiteSpace(input.Note)) bodyObject["Note"] = input.Note;
            if (!string.IsNullOrWhiteSpace(input.Project)) bodyObject["Project"] = input.Project;
            if (!string.IsNullOrWhiteSpace(input.Subject)) bodyObject["Subject"] = input.Subject;

            request.AddJsonBody(bodyObject);

            var response = await client.ExecuteWithErrorHandling(request);

            if (!response.IsSuccessful)
            {
                throw new PluginApplicationException($"Error updating term: {response.Content}");
            }

            return new UpdateTermResponse
            {
                Success = true,
                Message = "Term updated successfully"
            };
        }
        catch (Exception e)
        {
            throw new PluginApplicationException($"Error updating term: {e.Message}");
        }
    }
}