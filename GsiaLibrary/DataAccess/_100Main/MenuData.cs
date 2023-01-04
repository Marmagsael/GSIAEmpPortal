using GsiaLibrary.Models;
using GsiaLibrary.Models.UI._001Main;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Principal;

namespace GsiaLibrary.DataAccess._100Main;

public class MenuData : IMenuData
{
    private readonly IApiAccess _apiAccess;
    private readonly IConfiguration _config;

    public MenuData(IApiAccess apiAccess, IConfiguration config)
    {
        _apiAccess = apiAccess;
        _config = config;
    }

    public QueryResponseModel _10000_GetMenu()
    {
        string schema = _apiAccess.FetchDataFromApi("Login/0000/GetMainScheme").QueryResult!;
        string connName = _config.GetSection("ConnectionStrings:MySqlConn").Value;

        QueryResponseModel menu = _apiAccess.FetchDataFromApi($"Menu/GetMenu?schema={schema}");

        // --- CHECK SERVER CONNECTION -----------------------------------
        if (menu.Reponse == "connection failed")
        {
            menu.Description = "There's a problem connecting to the server.";
            menu.ErrorField = "Server";
            return menu;
        }
        else
        {
            menu.Description = "Successfully fetched the menu";
            menu.ErrorField = null;
            return menu;
        }

    }
}
