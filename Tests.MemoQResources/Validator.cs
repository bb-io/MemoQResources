using Apps.MemoQResources.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;
using MemoQResources.Base;

namespace Tests.MemoQResources
{
    [TestClass]
    public class Validator : TestBase
    {
        [TestMethod]
        public async Task ValidatesCorrectConnection()
        {
            var validator = new ConnectionValidator();

            var result = await validator.ValidateConnection(Creds, CancellationToken.None);
            Console.Write(result.Message);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public async Task DoesNotValidateIncorrectConnection()
        {
            var newCreds = Creds.Select(x => new AuthenticationCredentialsProvider(x.KeyName, x.Value + "_incorrect"));
            var validator = new ConnectionValidator();
            var result = await validator.ValidateConnection(newCreds, CancellationToken.None);
            Console.Write(result.Message);
            Assert.IsFalse(result.IsValid);
        }
    }
}