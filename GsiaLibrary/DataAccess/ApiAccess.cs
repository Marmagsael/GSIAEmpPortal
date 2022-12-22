
using GsiaLibrary.Models;
using GsiaLibrary.Models.FromApi.Login;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace GsiaLibrary.DataAccess;

public class ApiAccess : IApiAccess
{
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiAccess(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
    }


    public QueryResponseModel FetchDataFromApi(string ApiUrl)
    {
        var client = _httpClientFactory.CreateClient("api");
        using HttpResponseMessage response = client.GetAsync(ApiUrl).Result;
        QueryResponseModel QResponse = new QueryResponseModel();


        if (!response.IsSuccessStatusCode)
        {
            QResponse.Code = response.StatusCode.ToString();
            QResponse.Reponse = "connection failed";
            QResponse.Description = response.ReasonPhrase;
            QResponse.ErrorField = "server";
            QResponse.QueryResult = null;

        }
        else
        {
            QResponse.Code = response.StatusCode.ToString();
            QResponse.Reponse = "connection success";
            QResponse.Description = response.ReasonPhrase;
            QResponse.ErrorField = null;
            QResponse.QueryResult = response.Content.ReadAsStringAsync().Result;
        }

        return QResponse;
    }

    public async Task<QueryResponseModel> ExecuteDataFromApi<T>(string ApiUrl, T content)
    {
        //HttpResponseMessage response = client.PostAsync(client.BaseAddress + ApiUrl, content).Result;

        var client = _httpClientFactory.CreateClient("api");
        using HttpResponseMessage response = await client.PostAsJsonAsync(ApiUrl, content);
        QueryResponseModel QResponse = new QueryResponseModel();


        if (!response.IsSuccessStatusCode)
        {
            QResponse.Code = response.StatusCode.ToString();
            QResponse.Reponse = "connection failed";
            QResponse.Description = response.ReasonPhrase;
            QResponse.ErrorField = "server";
            QResponse.QueryResult = null;

        }
        else
        {
            QResponse.Code = response.StatusCode.ToString();
            QResponse.Reponse = "connection success";
            QResponse.Description = response.ReasonPhrase;
            QResponse.ErrorField = null;
            QResponse.QueryResult = await response.Content.ReadAsStringAsync();
        }

        return QResponse;
    }

}



