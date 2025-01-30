using RestSharp;

namespace Apps.MemoQResources.Api
{
    public class MemoQResourcesRequest : RestRequest
    {
        public MemoQResourcesRequest(string endpoint, Method method) : base(endpoint, method)
        {
        }
    }
}
