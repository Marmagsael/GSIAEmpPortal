
using GsiaLibrary.Models;
using GsiaLibrary.Models.FromApi.Login;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace GsiaLibrary.DataAccess;

public class ApiAccess : IApiAccess
{
    private readonly IConfiguration _config;

    public ApiAccess(IConfiguration config)
    {
        _config = config;
    }



    public QueryResponseModel FetchDataFromApi(string ApiUrl)
    {
        string uriAddress = _config.GetSection("ApiAddress").Value;
        Uri baseAddress = new Uri(uriAddress);

        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(client.BaseAddress + ApiUrl).Result;

        QueryResponseModel QResponse = new QueryResponseModel();


        if (!response.IsSuccessStatusCode)
        {
            QResponse.Code = response.StatusCode.ToString();
            QResponse.Reponse = "connection failed";
            QResponse.Description = response.ReasonPhrase;
            QResponse.QueryResult = null;

        }
        else
        {
            QResponse.Code = response.StatusCode.ToString();
            QResponse.Reponse = "connection success";
            QResponse.Description = response.ReasonPhrase;
            QResponse.QueryResult = response.Content.ReadAsStringAsync().Result;
        }

        return QResponse;
    }

}



