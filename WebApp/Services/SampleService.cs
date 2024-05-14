using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using WebAPIB2CSample.Models;

namespace WebApp.Services;

public static class SampleServiceExtensions
{
    public static void AddSampleService(this IServiceCollection services, IConfiguration configuration)
    {
        //Please take it as demo, doing this in production is crazy.
        services.AddHttpClient<ISampleService, SampleService>();
    }
}

public class SampleService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration)
    : ISampleService
{
    private readonly string _sampleScope = configuration["SampleList:SampleListScope"];
    private readonly string _sampleBaseAddress = configuration["SampleList:SampleListBaseAddress"];


    public async Task<IEnumerable<Sample>> GetAsync()
    {
        await PrepareAuthenticatedClient();

        var response = await httpClient.GetAsync($"{_sampleBaseAddress}/api/sample");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Sample>>(content); ;
        }

        throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
    }

    private async Task PrepareAuthenticatedClient()
    {
        var accessToken = await tokenAcquisition.GetAccessTokenForUserAsync(new[] { _sampleScope });
        Debug.WriteLine($"access token-{accessToken}");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}