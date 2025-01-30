﻿using System;
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
                EntryId = 19,
                Client = "Client Zhopa",

                Languages = new[] { "eng-GB", "ger" },
                Definition = new[] { "Hello", "Guten Tag" },
                Moderation = new[] { false, false },

                Text = new[] { "Helloasdasd", "Guten Tag" },
                Example = new[] { "Hi there!asdasdasdas", "Guten Tag!" },
                CaseSense = new[] { 1, 1 },
                TermIsForbidden = new[] { false, false },
                TermPartialMatches = new[] { 1, 1 },
                Note = "First greeting entry",
                Project = "Test Project",
                Subject = "Test Subject"
            };
            var response = await action.UpdateTerm(request);

            Assert.IsTrue(response.Success);
        }

    }
}
