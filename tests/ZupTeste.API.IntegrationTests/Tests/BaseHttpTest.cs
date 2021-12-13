using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using ZupTeste.API.Authentication;
using ZupTeste.API.Authentication.Contracts;

namespace ZupTeste.API.IntegrationTests.Tests;

public class BaseHttpTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    protected HttpClient HttpClient;
    protected readonly ITestOutputHelper Output;
        
    private readonly CustomWebApplicationFactory _factory;
    private string _token { get; set; }

    public BaseHttpTest(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        Output = output;
        HttpClient = factory.CreateClient();
    }
    protected async Task<TResponse> HttpGetAsync<TResponse>(
        string url,
        Dictionary<string, string> querystring = null,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var response = await HttpGetAsync(url, querystring, statusCode);

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    protected async Task<HttpResponseMessage> HttpGetAsync(
        string url,
        Dictionary<string, string> querystring = null,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (querystring != null)
            url += "?" + string.Join("&", querystring.Select(x => $"{x.Key}={x.Value}"));
            
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = CreateUri(url)
        };
            
        var response = await SendAsync(requestMessage);
            
        Assert.Equal(statusCode, response.StatusCode);

        return response;
    }

    protected async Task<TResponse> HttpPostAsync<TResponse>(
        string url, object body,
        Dictionary<string, string> querystring = null,
        HttpStatusCode statusCode = HttpStatusCode.Created)
    {
        var response = await HttpPostAsync(url, body, querystring, statusCode);

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    protected async Task<HttpResponseMessage> HttpPostAsync(
        string url, object body,
        Dictionary<string, string> querystring = null,
        HttpStatusCode statusCode = HttpStatusCode.Created)
    {
        if (querystring != null)
            url += "?" + string.Join("&", querystring.Select(x => $"{x.Key}={x.Value}"));
            
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = CreateUri(url),
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };
            
        var response = await SendAsync(requestMessage);

        Assert.Equal(statusCode, response.StatusCode);

        return response;
    }

    protected async Task<TResponse> HttpPutAsync<TResponse>(
        string url, object body,
        Dictionary<string, string> querystring = null,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var response = await HttpPutAsync(url, body, querystring, statusCode);

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    protected async Task<HttpResponseMessage> HttpPutAsync(
        string url, object body,
        Dictionary<string, string> querystring = null,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (querystring != null)
            url += "?" + string.Join("&", querystring.Select(x => $"{x.Key}={x.Value}"));

        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = CreateUri(url),
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };
            
        var response = await SendAsync(requestMessage);
            
        Assert.Equal(statusCode, response.StatusCode);

        return response;
    }

    protected async Task<object> HttpDeleteAsync(string url, HttpStatusCode statusCode = HttpStatusCode.NoContent)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = CreateUri(url)
        };
            
        var response = await SendAsync(requestMessage);
            
        Assert.Equal(statusCode, response.StatusCode);
            
        if(await response.Content.ReadAsByteArrayAsync() != Array.Empty<byte>())
            return await response.Content.ReadFromJsonAsync<object>();

        return null;
    }

    protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
    {
        Output.WriteLine($"[{DateTime.Now:G}] Request sent");
            
        requestMessage.Headers.Add("Authorization", $"Bearer {_token}");
            
        var response = await HttpClient.SendAsync(requestMessage);
            
        Output.WriteLine($"[{DateTime.Now:G}] [{requestMessage.Method}] " +
                         $"Getting response from {response.RequestMessage.RequestUri.PathAndQuery}");
            
        Output.WriteLine(await response.Content.ReadAsStringAsync());
            
        return response;
    }
        
    protected Uri CreateUri(string uri) => 
        string.IsNullOrEmpty(uri) ? null : new Uri(uri, UriKind.RelativeOrAbsolute);

    public async Task InitializeAsync()
    {
        var jwtHelper = _factory.ServiceProvider.GetService<JwtHelper>();
            
        var token = jwtHelper!.GenerateToken(new AdministradorAutenticado
        {
            Id = _factory.AdministradorPadrao.Id,
            Name = _factory.AdministradorPadrao.Nome,
            Email = _factory.AdministradorPadrao.Email
        });

        _token = token.AccessToken;
            
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {

        await Task.CompletedTask;
    }
}