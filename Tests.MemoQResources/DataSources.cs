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
            Assert.IsTrue(response.Count() > 0);
        }
    }
}
