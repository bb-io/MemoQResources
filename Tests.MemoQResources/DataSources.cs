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
            var action = new TermbaseEntryDataHandler(InvocationContext,new UpdateTermRequest { Guid= "468feef5-2a46-4b38-b8f3-03f1f9fceb80" });
            var response = await action.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
            foreach (var item in response)
            {
                Console.WriteLine($"{item.Value} - {item.DisplayName}");
                Assert.IsNotNull(item.Value);
            }
        }

    }
}
