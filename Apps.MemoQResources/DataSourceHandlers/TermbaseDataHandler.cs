using Apps.MemoQResources.Invocables;
using Apps.MemoQResources.Models.Items;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MemoQResources.DataSourceHandlers
{
    public class TermbaseDataHandler(InvocationContext invocationContext)
         : MemoQResourcesInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new RestRequest("memoqserverhttpapi/v1/tbs", Method.Get);

            var response = await Client.ExecuteWithErrorHandling<List<TermbaseModel>>(request);

            var items = response
                .Where(tb => string.IsNullOrEmpty(context.SearchString) || tb.FriendlyName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Select(tb => new DataSourceItem(tb.TBGuid, tb.FriendlyName));

            return items;
        }
    }
}
