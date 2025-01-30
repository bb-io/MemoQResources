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


        var termItem = new
        {
            CaseSense = input.CaseSense ?? null,
            Example = input.TermExample ?? null,
            IsForbidden = input.TermIsForbidden,
            PartialMatch = input.TermPartialMatches,
            Text = input.Text
        };

        var languageItem = new
        {
            Language = input.Language ?? null,
            Definition = input.Definition ?? null,
            NeedsModeration = input.Moderation,
            TermItems = new[] { termItem }
        };
        var username = InvocationContext.AuthenticationCredentialsProviders
        .First(p => p.KeyName == "username").Value;
        var bodyObject = new
        {
            Created = DateTime.UtcNow,
            Creator = "API",
            Modified = DateTime.UtcNow,
            Modifier = username,

            Client = input.Client ?? null,
            Domain = input.Domain ?? null,

            Languages = new[] { languageItem },

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