using Apps.MemoQResources.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.MemoQResources.Invocables;

public class MemoQResourcesInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected MemoQResourcesClient Client { get; }
    public MemoQResourcesInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new MemoQResourcesClient(Creds);
    }
}
