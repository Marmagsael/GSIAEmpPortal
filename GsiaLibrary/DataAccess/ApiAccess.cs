
using GsiaLibrary.Models;
using GsiaLibrary.Models.FromApi.Login;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;

namespace GsiaLibrary.DataAccess;

public class ApiAccess : IApiAccess
{
    private readonly IConfiguration _config;

    HttpClient client;

    public ApiAccess(IConfiguration config)
    {
        _config = config;
        string uriAddress = _config.GetSection("ApiAddress").Value;
        Uri baseAddress = new Uri(uriAddress);
        client = new HttpClient();
        client.BaseAddress = baseAddress;
    }




    public QueryResponseModel FetchDataFromApi(string ApiUrl)
    {

        HttpResponseMessage response = client.GetAsync(client.BaseAddress + ApiUrl).Result;

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

    public QueryResponseModel ExecuteDataFromApi(string ApiUrl, HttpContent content)
    {
        HttpResponseMessage response = client.PostAsync(client.BaseAddress + ApiUrl, content).Result;
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

}



