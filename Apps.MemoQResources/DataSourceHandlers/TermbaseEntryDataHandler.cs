using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.MemoQResources.Invocables;
using Apps.MemoQResources.Models.Items;
using Apps.MemoQResources.Models.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MemoQResources.DataSourceHandlers
{
    public class TermbaseEntryDataHandler : MemoQResourcesInvocable, IAsyncDataSourceItemHandler
    {
        private readonly UpdateTermRequest _input;

        public TermbaseEntryDataHandler(InvocationContext invocationContext,
        [ActionParameter] UpdateTermRequest input) : base(invocationContext)
        {
            _input = input;
        }

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
           
            var searchString = context.SearchString ?? "";
            var targetLanguage = _input.Language ?? "eng";

            var request = new RestRequest($"v1/tbs/{_input.Guid}/search", Method.Post);
            request.AddJsonBody(new
            {
                Condition = 1,
                SearchExpression = searchString,
                TargetLanguage = targetLanguage,
                Limit = 50 
            });

            var response = await Client.ExecuteWithErrorHandling<List<TBEntryModel>>(request);

            var items = response.Select(entry =>
                new DataSourceItem(entry.Id.ToString(), $"{entry.Client} - {entry.Project} - {entry.Subject}"));

            return items;
        }
    }
}
