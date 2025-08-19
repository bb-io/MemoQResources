using Apps.MemoQResources.Actions;
using Apps.MemoQResources.Models.Request;
using MemoQResources.Base;

namespace Tests.MemoQResources
{
    [TestClass]
    public class TermbaseTests : TestBase
    {
        [TestMethod]
        public async Task UpdateTermInTermbaseReturnsSuccess()
        {
            var action = new TermbaseActions(InvocationContext);
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

        [TestMethod]
        public async Task SearchTermbasesReturnsSuccess()
        {
            var action = new TermbaseActions(InvocationContext);
            var request = new ListTermBaseRequest
            { Languages = new[] { "eng" } };

            var response = await action.SearchTermbases(request);
            var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task DeleteTermbaseEntryReturnsSuccess()
        {
            var action = new TermbaseActions(InvocationContext);
            var request = new DeleteTermbaseEntryRequest
            { Guid = "468feef5-2a46-4b38-b8f3-03f1f9fceb80", EntryId = "2" };

            var response = await action.DeleteTermbaseEntry(request);
            var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task CreateTermbaseEntryReturnsSuccess()
        {
            var action = new TermbaseActions(InvocationContext);
            var request = new CreateTermbaseEntryRequest
            {
                Guid = "468feef5-2a46-4b38-b8f3-03f1f9fceb80",
                Client = "CLient",
                Domain = "Domain",
                Note = "First greeting entry maybe",
                Project = "Na-Test",
                Subject = "Best project ",
                Language = "eng",
                Text = "Hello aligator127",
                Definition = "Hello",
                Example = "HELLO WHOLE NEW TEXT",
                CaseSense = "1",
                PartialMatch = "1",
                IsForbidden = false,
                NeedsModeration = false
            };

            var response = await action.CreateTerm(request);
            var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
