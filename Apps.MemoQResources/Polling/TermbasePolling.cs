using Apps.MemoQResources.Api;
using Apps.MemoQResources.Invocables;
using Apps.MemoQResources.Polling.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.MemoQResources.Polling
{
    [PollingEventList]
    public class TermbasePolling : MemoQResourcesInvocable
    {
        public TermbasePolling(InvocationContext invocationContext) : base(invocationContext) { }

        [PollingEvent("On terms added or updated", "Triggered when new terms are added or existing terms are updated")]
        public async Task<PollingEventResponse<TermbaseMemory, TermbaseUpdateResponse>> OnTermsAddedOrUpdated(
            PollingEventRequest<TermbaseMemory> request,
            [PollingEventParameter] TermbaseInput input)
        {
            var client = new MemoQResourcesClient(InvocationContext.AuthenticationCredentialsProviders);
            var requestUrl = $"memoqserverhttpapi/v1/tbs/{input.TbId}";

            var restRequest = new RestRequest(requestUrl, Method.Get);
            var response = await client.ExecuteAsync<TermbaseUpdateResponse>(restRequest);

            if (!response.IsSuccessful || response.Data == null)
            {
                return new PollingEventResponse<TermbaseMemory, TermbaseUpdateResponse>
                {
                    Memory = request.Memory,
                    FlyBird = false
                };
            }

            var lastModified = response.Data.LastModified;
            var lastPollingTime = request.Memory?.LastPollingTime ?? DateTime.MinValue;

            if (lastModified > lastPollingTime)
            {
                return new PollingEventResponse<TermbaseMemory, TermbaseUpdateResponse>
                {
                    Memory = new TermbaseMemory { LastPollingTime = lastModified, Triggered = true },
                    Result = response.Data,
                    FlyBird = true
                };
            }

            return new PollingEventResponse<TermbaseMemory, TermbaseUpdateResponse>
            {
                Memory = request.Memory,
                FlyBird = false
            };
        }


      
    }
}
