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
           
            var targetLanguage = _input.Languages;

            var request = new RestRequest($"memoqserverhttpapi/v1/tbs/{_input.Guid}/search", Method.Post);
            request.AddJsonBody(new
            {
                Condition = _input.Condition,
                SearchExpression = _input.SearchExpression,
                TargetLanguage = new string[] { _input.Languages.First() },
                Limit = 50 
            });

            var response = await Client.ExecuteWithErrorHandling<List<TBEntryModel>>(request);

            var items = response.Select(entry =>
                new DataSourceItem(entry.Id.ToString(), $"{entry.Client} - {entry.Project} - {entry.Subject}"));

            return items;
        }
    }
}
