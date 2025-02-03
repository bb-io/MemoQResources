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
                EntryId = "26",
                Client = "CLient",
                Language = "ger",
                Definition = "Hello",
                Moderation = false,
                Text = "Hello aligator",
                Example = "WHOLE NEW TEXT",
                CaseSense = 1,
                TermIsForbidden = false,
                TermPartialMatches = 1,
                Note = "First greeting entry maybe",
                Project = "Best project ",
                Subject = "Best project "
            };
            var response = await action.UpdateTerm(request);

            Assert.IsNotNull(response);
            Console.WriteLine($"{response.UpdatedEntry.Id} - {response.UpdatedEntry.Note} -  {response.UpdatedEntry.Project}");
        }
    }
}
