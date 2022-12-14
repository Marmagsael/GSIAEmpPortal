
using GsiaLibrary.Models;
using GsiaLibrary.Models.FromApi.Login;
using GsiaLibrary.Models.UI.Login;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace GsiaLibrary.DataAccess.Login;

public class LoginData : ILoginData
{
    private readonly IApiAccess _apiAccess;
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;


    public LoginData(IApiAccess apiAccess, IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _apiAccess = apiAccess;
        _config = config;
        _httpClientFactory = httpClientFactory;
    }

    public QueryResponseModel _10000_ValidateEmployeeByLoginNameAndPassword(LoginInputModel? input)
    {
        string loginName = input?.EmpNumber!;
        string pwd = input?.Password!;


        QueryResponseModel userFromMain = _apiAccess.FetchDataFromApi("/Login/1002/GetUser/" + loginName);
        // --- CHECK SERVER CONNECTION -----------------------------------
        if (userFromMain.Reponse == "connection failed")
        {
            userFromMain.Description = "There's a problem connecting to the server";
            userFromMain.ErrorField = "Server";
            return userFromMain;
        }
        else
        {
            //--- CHECK IF EMPLOYEE NUMBER EXISTS IN MAIN TABLE -----------------------------------
            if (userFromMain.QueryResult.Length == 0)
            {

                //  EMPLOYEE DOES NOT EXIST IN MAIN TABLE 
                userFromMain.Description = "Employee number does not exist. Please register.";
                userFromMain.ErrorField = "EmployeeNo";
                return userFromMain;

            }
            else
            {
                //  EMPLOYEE EXISTS IN MAIN TABLE THEN  CHECK EMPLOYEE'S PASSWORD ---------------------------------------
                // CHECK IF  USER PROVIDE A PASSWORD
                if (pwd == null)
                {
                    userFromMain.Description = "Please provide your password.";
                    userFromMain.ErrorField = "Password";
                    return userFromMain;
                }
                else
                {
                    userFromMain = _apiAccess.FetchDataFromApi("/Login/1000/userlogin/" + loginName + "/" + pwd);

                    // --- CHECK SERVER CONNECTION -----------------------------------
                    if (userFromMain.Reponse == "connection failed")
                    {
                        userFromMain.Description = "There's a problem connecting to the server.";
                        userFromMain.ErrorField = "Server";
                        return userFromMain;
                    }
                    else
                    {
                        //CHECK IF  THE PASSWORD IS CORRECT
                        if (userFromMain.QueryResult.Length == 0)
                        {
                            userFromMain.Description = "You have entered an invalid password. Please try again.";
                            userFromMain.ErrorField = "Password";
                            return userFromMain;
                        }
                        else
                        {
                            userFromMain.Description = "Password Match";
                            userFromMain.ErrorField = null;
                            return userFromMain;
                        }
                    }
                }

            }
        }
    }

    public QueryResponseModel _20000_ValidateEmployeeByEmail(string email)
    {

        string Email = email; ;
        string schema = GetPisScheme();
        string ConnName = "MySqlConn";

        QueryResponseModel? userFromMain = _apiAccess.FetchDataFromApi("/Login/1003/GetUser/" + Email);

        // CHECK SERVER CONNECTION -------------------------------
        if (userFromMain.Reponse == "connection failed")
        {
            userFromMain.Description = "There's a problem connecting to the server";
            userFromMain.ErrorField = "Server";
            return userFromMain;
        }
        else
        {
            //CHECK IF USER EMAIL EXISTS IN THE MAIN TABLE
            if (userFromMain.QueryResult.Length == 0)
            {
                userFromMain.Description = "Email don't exist";
                userFromMain.ErrorField = null;
                return userFromMain;
            }
            else
            {
                userFromMain.Description = "Email exists";
                userFromMain.ErrorField = null;
                return userFromMain;
            }
        }

    }
    private string GetMainScheme()
    {
        QueryResponseModel getSchema = _apiAccess.FetchDataFromApi("/Login/0000/GetMainScheme");
        string schema = getSchema.QueryResult;
        return schema;
    }
    private string GetPisScheme()
    {
        QueryResponseModel getSchema = _apiAccess.FetchDataFromApi("/Login/0000/GetPisScheme");
        string schema = getSchema.QueryResult;
        return schema;
    }
    public string GetCompanyInfo() // temporary
    {
        var CoName = _config.GetSection("CompanyInfo:CompanyName").Value;
        return CoName;
    }

}
