using Apps.MemoQResources.Api;
using Apps.MemoQResources.Invocables;
using Apps.MemoQResources.Models.Request;
using Apps.MemoQResources.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MemoQResources.Actions;

[ActionList]
public class TermActions: BaseInvocable
{
    public TermActions(InvocationContext invocationContext) : base(invocationContext)
    {      
    }

    //[Action("Update term", Description = "Updates a termbase entry in memoQ")]
    //public async Task<UpdateTermResponse> UpdateTerm([ActionParameter] UpdateTermRequest input)
    //{

    //}
}