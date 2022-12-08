
using GsiaLibrary.Models;
using GsiaLibrary.Models.FromApi.Login;
using GsiaLibrary.Models.UI;
using Newtonsoft.Json;
using System.Reflection;

namespace GsiaLibrary.DataAccess.Login;

public class Login
{
    private readonly IApiAccess _apiAccess;

    public Login(IApiAccess apiAccess)
    {
        _apiAccess = apiAccess;
    }

    public QueryResponseModel ValidateInitialCredential(LoginInputModel? input)
    {
        string loginName = input?.EmpNumber!;
        string pwd = input?.Password!;

        QueryResponseModel userFromMain = _apiAccess.FetchDataFromApi("/Login/1002/GetUser/" + loginName);

        // --- Check Connection to Server -----------------------------------
        //if (userFromMain.Reponse == "connection failed")
        //{
        //    userFromMain.Description = "There's a problem connecting to the server"; 
        //    return userFromMain;
        //}

        //// --- Check if User Exists in Main Table -----------------------------------
        //if(userFromMain.QueryResult==null)
        //{
        //    // --- Check in PIS 

        //} else
        //{
        //    //Check valid UName Password 

        //}


        return userFromMain;
    }

    

}
