using Apps.MemoQResources.Models.Items;
using Apps.MemoQResources.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.MemoQResources.Api;

public class MemoQResourcesClient : BlackBirdRestClient
{
    private readonly IEnumerable<AuthenticationCredentialsProvider> _creds;
    private string? _token;

    public MemoQResourcesClient(IEnumerable<AuthenticationCredentialsProvider> creds)
         :base(new RestClientOptions { ThrowOnAnyError = false, BaseUrl = new Uri(creds.First(p => p.KeyName == "url").Value) })
    {
        var token = GetAccessToken(creds);
        this.AddDefaultHeader("Authorization", $"MQS-API {token}");
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        string content = (await ExecuteWithErrorHandling(request)).Content;
        T val = JsonConvert.DeserializeObject<T>(content, JsonSettings);
        if (val == null)
        {
            throw new PluginApplicationException($"Could not parse {content} to {typeof(T)}");
        }

        return val;
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        RestResponse restResponse = await ExecuteAsync(request);
        if (!restResponse.IsSuccessStatusCode)
        {
            throw ConfigureErrorException(restResponse);
        }
        return restResponse;
    }


    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var error = JsonConvert.DeserializeObject<ErrorDto>(response.Content, JsonSettings);
        return new PluginApplicationException($"Error message: {response.Content}; StatusCode: {response.StatusCode}");
    }

    private string GetAccessToken(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var username = creds.First(p => p.KeyName == "username").Value;
        var password = creds.First(p => p.KeyName == "password").Value;
        var loginMode = creds.FirstOrDefault(p => p.KeyName == "loginMode")?.Value ?? "0";

        var loginRequest = new RestRequest("memoqserverhttpapi/v1/auth/login", Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(new
            {
                UserName = username,
                Password = password,
                LoginMode = loginMode
            });

        var response = this.Execute(loginRequest);

        if (!response.IsSuccessful)
        {
            throw new PluginApplicationException(
                $"Failed to login to MemoQ. Status code: {response.StatusCode}, " +
                $"Error: {response.Content}");
        }

        var tokenResponse = JsonConvert.DeserializeObject<MemoQLoginResponse>(response.Content, JsonSettings);
        if (tokenResponse?.AccessToken == null)
            throw new PluginApplicationException("Login succeeded but token was not found in the response.");

        return tokenResponse.AccessToken;
    }
}