using Apps.MemoQResources.DataSourceHandlers;
using Apps.MemoQResources.Models.Request;
using Blackbird.Applications.Sdk.Common.Dynamic;
using MemoQResources.Base;

namespace Tests.MemoQResources
{
    [TestClass]
    public class DataSources : TestBase
    {
        [TestMethod]
        public async Task TermbaseHandlerReturnsValues()
        {
            var action = new TermbaseDataHandler(InvocationContext);
            var response = await action.GetDataAsync(new DataSourceContext { SearchString=""}, CancellationToken.None);
            foreach (var item in response)
            {
                Console.WriteLine($"{ item.Value} - {item.DisplayName}");
                Assert.IsNotNull(item.Value);
            }
        }


        [TestMethod]
        public async Task EntriesHandlerReturnsValues()
        {
            var action = new TermbaseEntryDataHandler(InvocationContext,new UpdateTermRequest {Languages= ["eng-GB"], Condition=0, Guid= "1366ac93-cf7c-46f1-80da-b0d15ec22c29", SearchExpression="Hello"});
            var response = await action.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
            foreach (var item in response)
            {
                Console.WriteLine($"{item.Value} - {item.DisplayName}");
                Assert.IsNotNull(item.Value);
            }
        }
    }
}
