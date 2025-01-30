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
            var action = new TermbaseEntryDataHandler(InvocationContext,new UpdateTermRequest {Language= "eng-GB", Condition=0, Guid= "639a061a-6c95-41ef-91b9-379b4db519ae", SearchExpression="Hello" });
            var response = await action.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
            foreach (var item in response)
            {
                Console.WriteLine($"{item.Value} - {item.DisplayName}");
                Assert.IsNotNull(item.Value);
            }
        }

    }
}
