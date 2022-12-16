﻿
using GsiaLibrary.Models;
using GsiaLibrary.Models.FromApi.Login;
using GsiaLibrary.Models.UI.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
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
            userFromMain.Description = "There's a problem connecting to the server.";
            userFromMain.ErrorField = "Server";
            return userFromMain;
        }
        else
        {
            //--- CHECK IF EMPLOYEE NUMBER EXISTS IN THE MAIN TABLE -----------------------------------
            if (userFromMain.QueryResult.Length == 0)
            {

                //  EMPLOYEE DOES NOT EXIST -----------------------------------------------------------
                userFromMain.Description = "Employee number does not exist. Please register.";
                userFromMain.ErrorField = "EmployeeNo";
                return userFromMain;

            }
            else
            {
                //  EMPLOYEE EXISTS ------------------------------------------------------------------
                // CHECK IF  USER PROVIDE A PASSWORD -------------------------------------------------
                if (pwd == null)
                {
                    userFromMain.Description = "Please provide your password.";
                    userFromMain.ErrorField = "Password";
                    return userFromMain;
                }
                else
                {
                    userFromMain = _apiAccess.FetchDataFromApi("/Login/1000/userlogin/" + loginName + "/" + pwd);

                    // --- CHECK SERVER CONNECTION --------------------------------------------------
                    if (userFromMain.Reponse == "connection failed")
                    {
                        userFromMain.Description = "There's a problem connecting to the server.";
                        userFromMain.ErrorField = "Server";
                        return userFromMain;
                    }
                    else
                    {
                        //CHECK IF  THE PASSWORD IS CORRECT --------------------------------------------
                        if (userFromMain?.QueryResult!.Length == 0)
                        {
                            // INCORRECT PASSWORD ------------------------------------------------------
                            userFromMain.Description = "You have entered an invalid password. Please try again.";
                            userFromMain.ErrorField = "Password";
                            return userFromMain;
                        }
                        else
                        {
                            // PASSWORD MATCH! --------------------------------------------------------
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

        QueryResponseModel? userFromMain = _apiAccess.FetchDataFromApi("/Login/1003/GetUser/" + Email);

        // CHECK SERVER CONNECTION ------------------------------------------------------------------
        if (userFromMain.Reponse == "connection failed")
        {
            userFromMain.Description = "There's a problem connecting to the server.";
            userFromMain.ErrorField = "Server";
            return userFromMain;
        }
        else
        {
            //CHECK IF EMAIL EXISTS IN THE MAIN TABLE -----------------------------------------------
            if (userFromMain.QueryResult.Length == 0)
            {
                // EMAIL DOESN'T EXIST --------------------------------------------------------------
                userFromMain.Description = "Email don't exist";
                userFromMain.ErrorField = null;
                return userFromMain;
            }
            else
            {
                // EMAIL EXISTS ---------------------------------------------------------------------
                userFromMain.Description = "Email exists";
                userFromMain.ErrorField = null;
                return userFromMain;
            }
        }

    }


    public QueryResponseModel _3000_RegisterAccount(RegisterInputModel input)
    {
        string empNumber = input?.EmpNumber!;
        string depCode = input?.DepCode!; // ASK THE EQUIVALENT FIELD NAME IN DATABASE
        string movNumber = input?.MovNumber!;
        string secLicense = input?.SecLicense!;
        string dateHired = input?.DateHired!;
        string password = input?.Password!;
        string email = input.Email;
        string schema = GetPisScheme();




        QueryResponseModel userFromMain = _apiAccess.FetchDataFromApi("/Login/1002/GetUser/" + empNumber);
        // --- CHECK SERVER CONNECTION ---------------------------------------------------------------
        if (userFromMain.Reponse == "connection failed")
        {
            userFromMain.Description = "There's a problem connecting to the server.";
            userFromMain.ErrorField = "Server";
            return userFromMain;
        }
        else
        {
            //--- CHECK IF EMPLOYEE NUMBER EXISTS IN THE MAIN TABLE ---------------------------------------
            if (userFromMain.QueryResult.Length != 0)
            {

                //  EMPLOYEE EXISTS IN MAIN TABLE 
                userFromMain.Description = "Employee number already exists. Please try to sign-in instead.";
                userFromMain.ErrorField = "EmployeeNo";
                return userFromMain;

            }
            else
            {
                // EMPLOYEE NOT EXIST IN MAIN TABLE ---------------------------------------------------------
                // CHECK IF CREDENTIALS MATCHES WITH RECORDS IN SECPIS  -------------------------------------

                userFromMain.ErrorField = email;
                if (email == null) { email = "null"; } // ******* TEMPORARY *************************************

                QueryResponseModel userFromSecpis = _apiAccess.FetchDataFromApi("/Login/1004/validateUserFromEmpmas/" + empNumber + "/" + dateHired + "/" + secLicense + "/" + movNumber + "/" + schema + "?connName=MySqlConn");
                // --- CHECK SERVER CONNECTION ---------------------------------------------------------------
                if (userFromSecpis.Reponse == "connection failed")
                {
                    userFromSecpis.Description = "There's a problem connecting to the server.";
                    userFromSecpis.ErrorField = "Server";
                    return userFromSecpis;
                }
                else
                {

                    if (userFromSecpis.QueryResult.Length == 0)
                    {
                        // INVALID CREDENTIALS --------------------------------------------------------------
                        userFromSecpis.Description = "Credentials do not match our record.";
                        userFromSecpis.ErrorField = "annonymous";
                        return userFromSecpis;
                    }
                    else
                    {
                        // VALID CREDENTIALS ---------------------------------------------------------------
                        string domain = "gsiaph.info";
                        schema = GetMainScheme();
                        string connName = "MySqlConn";

                        UserMainModel mainModel = new()
                        {
                            LoginName = empNumber,
                            Password = password,
                            Email = email,
                            Domain = "domain"
                        };

                        //CONVERT DATA TO JSON -------------------------------------------------------------
                        string data = JsonConvert.SerializeObject(mainModel);
                        StringContent content = new(data, Encoding.UTF8, "application/json");


                        // INSERT RECORDS TO MAIN SCHEMA ---------------------------------------------------
                        QueryResponseModel userInsertToMain = _apiAccess.ExecuteDataFromApi("/Login/1004/InsertUserFromEmpmas/" + empNumber + "/" + password + "/" + email + "/" + domain + "/" + schema + "/" + connName, content);


                        // --- CHECK SERVER CONNECTION ------------------------------------------------------
                        if (userInsertToMain.Reponse == "connection failed")
                        {
                            userInsertToMain.Description = "There's a problem connecting to the server.";
                            userInsertToMain.ErrorField = "Server2";
                            return userInsertToMain;
                        }
                        // RECORDS HAVE BEEN INSERTED IN MAIN SUCCESSFULLY -----------------------------------
                        userInsertToMain.Description = "Successfully registered the user";
                        userInsertToMain.ErrorField = null;
                        return userInsertToMain;
                    }


                }


            }
        }
    }

    private string GetMainScheme()
    {
        QueryResponseModel getSchema = _apiAccess.FetchDataFromApi("/Login/0000/GetMainScheme");
        string schema = getSchema?.QueryResult!;
        return schema;
    }
    private string GetPisScheme()
    {
        QueryResponseModel getSchema = _apiAccess.FetchDataFromApi("/Login/0000/GetPisScheme");
        string schema = getSchema?.QueryResult!;
        return schema;
    }
    public string GetCompanyInfo() // temporary
    {
        var CoName = _config.GetSection("CompanyInfo:CompanyName").Value;
        return CoName;
    }

}
