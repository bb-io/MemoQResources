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
public class TermActions: BaseInvocable
{
    public TermActions(InvocationContext invocationContext) : base(invocationContext)
    {      
    }

    [Action("Update term", Description = "Updates a termbase entry in memoQ")]
    public async Task<UpdateTermResponse> UpdateTerm([ActionParameter] UpdateTermRequest input)
    {
        var client = new MemoQResourcesClient(InvocationContext.AuthenticationCredentialsProviders);
        var request = new RestRequest(
            $"memoqserverhttpapi/v1/tbs/{input.Guid}/entries/{input.EntryId}/update", Method.Post);
        var username = InvocationContext.AuthenticationCredentialsProviders
                    .First(p => p.KeyName == "username").Value;
        var languagesList = input.Languages.ToList();
        var definitionsList = input.Definition.ToList();
        var moderationList = input.Moderation.ToList();
        var textsList = input.Text.ToList();
        var examplesList = input.Example.ToList();
        var caseSensitivitiesList = input.CaseSense.ToList();
        var isForbiddenList = input.TermIsForbidden.ToList();
        var partialMatchesList = input.TermPartialMatches.ToList();

        var languages = new List<object>();

        for (int i = 0; i < languagesList.Count; i++)
        {
            var termItems = new List<object>
        {
            new
            {
                CaseSense = caseSensitivitiesList.ElementAtOrDefault(i),
                Example = examplesList.ElementAtOrDefault(i),
                IsForbidden = isForbiddenList.ElementAtOrDefault(i),
                PartialMatch = partialMatchesList.ElementAtOrDefault(i),
                Text = textsList.ElementAtOrDefault(i)
            }
        };

            var languageItem = new
            {
                Language = languagesList[i],
                Definition = definitionsList.ElementAtOrDefault(i),
                NeedsModeration = moderationList.ElementAtOrDefault(i),
                TermItems = termItems
            };

            languages.Add(languageItem);
        }

        var bodyObject = new
        {
            Created = DateTime.UtcNow,
            Creator = "API",
            Modified = DateTime.UtcNow,
            Modifier = username,
            Client = input.Client ?? null,
            Domain = input.Domain ?? null,
            Languages = languages,
            Note = input.Note ?? null,
            Project = input.Project ?? null,
            Subject = input.Subject ?? null
        };

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
}