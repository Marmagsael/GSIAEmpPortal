
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

    public QueryResponseModel ValidateEmployeeByLoginNameAndPassword(LoginInputModel? input)
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
            //--- CHECK IF USER EXIST IN SECPIS TABLE -----------------------------------
            if (userFromMain.QueryResult.Length == 0)
            {

                string schema = GetPisScheme();
                QueryResponseModel userFromSecpis = _apiAccess.FetchDataFromApi("/Login/0001/EmpmasByEmpnumber/" + loginName + "/" + schema + "/MySqlConn");

                if (userFromSecpis.QueryResult.Length == 0)
                {
                    //  EMPLOYEE NOT EXIST IN SECPIS TABLE 
                    userFromSecpis.Description = "Employe Number was not found";
                    userFromSecpis.ErrorField = "EmployeeNo";
                    return userFromSecpis;
                }
                else
                {
                    //  EMPLOYEE  EXIST IN SECPIS TABLE 
                    userFromSecpis.Description = "Employee number exists in secpis";
                    userFromSecpis.ErrorField = null;
                    return userFromSecpis;
                }

            }
            else
            {
                //--- VALIDATE PASSWORD IN MAIN TABLE---
                userFromMain = _apiAccess.FetchDataFromApi("/Login/1000/userlogin/" + loginName + "/" + pwd);
                if (userFromMain.QueryResult == null)
                {
                    // NO PASSWORD WAS PROVIDED
                    userFromMain.Description = "Please provide a password";
                    userFromMain.ErrorField = "Password";
                    return userFromMain;
                }
                else
                {
                    if (userFromMain.QueryResult.Length == 0)
                    {
                        // PASSWORD DID NOT MATCH
                        userFromMain.Description = "Incorrect Password";
                        userFromMain.ErrorField = "Password";
                        return userFromMain;
                    }
                    else
                    {
                        // PASSWORD MATCH
                        userFromMain.Description = "Password Match";
                        userFromMain.ErrorField = null;
                        return userFromMain;
                    }
                }
            }
        }
    }
    public QueryResponseModel ValidateEmployeeByAddedCredentials(VerifyAccountInputModel? input)
    {
        string EmpNumber = input.vEmpNumber;
        string DateHired = input.DateHired;
        string SecLicense = input.SecLicense;
        string MovNumber = input.MovNumber;
        string Email = input.Email + "@gmail.com";
        string Password = input.vPassword;
        string schema = GetPisScheme();
        string ConnName = "MySqlConn";


        QueryResponseModel? userFromSecpis = _apiAccess.FetchDataFromApi("/Login/1004/validateUserFromEmpmas/" + EmpNumber + "/" + DateHired + "/" + SecLicense + "/" + MovNumber + "/" + schema + "?connName=MySqlConn");

        // CHECK SERVER CONNECTION -------------------------------
        if (userFromSecpis.Reponse == "connection failed")
        {
            userFromSecpis.Description = "There's a problem connecting to the server";
            userFromSecpis.ErrorField = "Server";
            return userFromSecpis;
        }
        else
        {
            //--- CHECK IF USER'S INPUT MATCHES WITH THE DATA IN EMPMAS TABLE -----------------------------------
            if (userFromSecpis.QueryResult.Length == 0)
            {
                // CREDENTIALS DO NOT MATCH WITH THE RECORD IN PIS
                userFromSecpis.Description = "Credentials do not match the record";
                userFromSecpis.ErrorField = "annonymous";
                return userFromSecpis;

            }
            else
            {
                // CREDENTIALS MATCH WITH THE RECORD IN PIS 
                UserMainModel mainModel = new()
                {
                    LoginName = EmpNumber,
                    Password = Password,
                    Email = Email,
                    Domain = "gsiaph.info"
                };

                schema = GetMainScheme();

                //  INSERT CREDENTIALS IN MAIN SCHEMA 
                string data = JsonConvert.SerializeObject(mainModel);
                StringContent content = new(data, Encoding.UTF8, "application/json");

                userFromSecpis = _apiAccess.ExecuteDataFromApi("/Login/1004/InsertUserFromEmpmas/" + EmpNumber + "/" + Password + "/" + Email + "/" + mainModel.Domain + "/" + schema + "/" + ConnName, content);

                return userFromSecpis;
            }
        }


    }
    public QueryResponseModel ValidateEmployeeByEmail(string email)
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
        } else {
            //CHECK IF USER EMAIL EXISTS IN THE MAIN TABLE
            if (userFromMain.QueryResult.Length == 0)
            {
                userFromMain.Description = "Email don't exist";
                userFromMain.ErrorField = null;
                return userFromMain;
            } else
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
