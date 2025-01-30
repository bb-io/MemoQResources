using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.MemoQResources.Actions;
using Apps.MemoQResources.DataSourceHandlers;
using Apps.MemoQResources.Models.Request;
using Blackbird.Applications.Sdk.Common.Dynamic;
using MemoQResources.Base;

namespace Tests.MemoQResources
{
    [TestClass]
    public class TermbaseTests : TestBase
    {
        [TestMethod]
        public async Task UpdateTermInTermbaseReturnsSuccess()
        {
           var action = new TermActions(InvocationContext);
            var request = new UpdateTermRequest
            {
                Guid = "1366ac93-cf7c-46f1-80da-b0d15ec22c29",
                Condition = 0,
                Language = "eng-GB",
                SearchExpression = "Hello",
                EntryId = 19,
                Text = "Hello how are you?",
                CaseSense = 1,
                Definition = "Hello Chumba",
                TermIsForbidden = false,
                Note = "First greeting entry",
                Moderation = true,
                TermPartialMatches = 0,
                Modified = DateTime.UtcNow,
                TermExample = "Hello how are you bashka?",
                Client = "MyClient"
            };
            var response = await action.UpdateTerm(request);

            Assert.IsTrue(response.Success);
        }

    }
}
