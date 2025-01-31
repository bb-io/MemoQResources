using Apps.MemoQResources.Polling;
using Apps.MemoQResources.Polling.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using MemoQResources.Base;

namespace Tests.MemoQResources
{
    [TestClass]
    public class PollingTests : TestBase
    {
        [TestMethod]
        public async Task OnTermbaseUpdated_WithPolling_ReturnsTrue()
        {
            var lastPollingTime = DateTime.UtcNow.AddHours(-1);
            var pollingHandler = new TermbasePolling(InvocationContext);
            var request = new PollingEventRequest<TermbaseMemory>
            {
                Memory = new TermbaseMemory { LastPollingTime = lastPollingTime }
            };

            var input = new TermbaseInput { TbId = "1366ac93-cf7c-46f1-80da-b0d15ec22c29" };
            var result = await pollingHandler.OnTermsAddedOrUpdated(request, input);

            Assert.IsTrue(result.FlyBird);
            Assert.IsTrue(result.Memory.Triggered);
            Assert.IsNotNull(result.Memory.LastPollingTime);
            Console.WriteLine($"LastPollingTime: {result.Memory.LastPollingTime}");
        }


        [TestMethod]
        public async Task OnTermbaseUpdated_WithPolling_ReturnsFalse()
        {
            var lastPollingTime = DateTime.UtcNow;
            var pollingHandler = new TermbasePolling(InvocationContext);
            var request = new PollingEventRequest<TermbaseMemory>
            {
                Memory = new TermbaseMemory { LastPollingTime = lastPollingTime }
            };
            var input = new TermbaseInput { TbId = "1366ac93-cf7c-46f1-80da-b0d15ec22c29" };
            var result = await pollingHandler.OnTermsAddedOrUpdated(request, input);
            Assert.IsFalse(result.FlyBird);
            Assert.IsFalse(result.Memory.Triggered);
            Console.WriteLine($"LastPollingTime: {result.Memory.LastPollingTime}");
        }
    }
}
