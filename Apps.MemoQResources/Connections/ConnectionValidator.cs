using Apps.MemoQResources.Api;
using Apps.MemoQResources.Models.Items;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.MemoQResources.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {

        try
        {
            var client = new MemoQResourcesClient(authenticationCredentialsProviders);
            var request = new RestRequest("/tbs", Method.Get);

            var response = await client.ExecuteWithErrorHandling<List<TermbaseModel>>(request);
            return new ConnectionValidationResponse
            {
                IsValid = true
            };
        }
        catch (Exception e)
        {
            return new ConnectionValidationResponse
            {
                IsValid = false,
                Message = e.Message,
            };
        }
    }
}