using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.MemoQResources.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "MemoQ Resources API connection (auto-login)",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new("url")
                {
                    DisplayName = "Server URL",
                    Description = "For example: https://my-memoq-server:8081/memoqserverhttpapi"
                },
                new("username")
                {
                    DisplayName = "Username",
                },
                new("password")
                {
                    DisplayName = "Password",
                    Sensitive = true
                },
                new("loginMode")
                {
                    DisplayName = "Login Mode",
                    Description = "0 = MemoQServerUser, 1 = WindowsUser, 2 = LanguageTerminal, 3 = OidcUser (by default 0)."
                }
                //dropdown add (check the need of login mode)
        }   }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        yield return new AuthenticationCredentialsProvider("url", values["url"]);
        yield return new AuthenticationCredentialsProvider("username", values["username"]);
        yield return new AuthenticationCredentialsProvider("password", values["password"]);

        var loginMode = values.ContainsKey("loginMode") ? values["loginMode"] : "0";
        yield return new AuthenticationCredentialsProvider("loginMode", loginMode);
    }
}