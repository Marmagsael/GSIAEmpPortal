using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using LibraryMySql.Models;
using LibraryMySql.DataAccess.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryMySql;
using Dapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using GSIA.Models;
using MysqlApiLibrary.Models.Login;
using System.Linq.Expressions;
using MySqlX.XDevAPI;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GSIA.Controllers;

public class LoginController : Controller
{

    private readonly IConfiguration _configuration;
    private readonly ILoginAccess _login;
    private readonly IMySqlDataAccess _mysql;
    private static string _empNumber = "";


    Uri baseAddress = new Uri("https://localhost:7087/api");
    HttpClient client;

    public LoginController(IConfiguration configuration, ILoginAccess login, IMySqlDataAccess mysql)
    {
        _configuration = configuration;
        _login = login;
        _mysql = mysql;
        client = new HttpClient();
        client.BaseAddress = baseAddress;

    }
    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> ValidateInitialCredential(LoginInputModel? input)
    {
        string loginName = input?.EmpNumber;
        string pwd = input?.Password;
        return Ok(_0001_CheckIfUserExists(loginName));


    }

    private string _0000_HttpResponse(string urlWithParams)
    {
        HttpResponseMessage response = client.GetAsync(client.BaseAddress + urlWithParams).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            return data;
        }
        return "server error";
    }
    private bool _0001_CheckIfUserExists(string loginName)
    {
        UserMainModel model = new UserMainModel();
        var data = _0000_HttpResponse("/Login/1002/GetUser//" + loginName);
        model = JsonConvert.DeserializeObject<UserMainModel?>(data);
        if (model != null)
        {
            return true;

        }
        return false;
    }
   
    private string _0002_ValidateUNameAndPwd(string loginName, string pwd)
    {
        UserMainModel model = new UserMainModel();
        HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Login/1000/userlogin/" + loginName + "/" + pwd).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            model = JsonConvert.DeserializeObject<UserMainModel?>(data);

            if (model != null)
            {
                return "exist";
            }
            return "notExist";
        }
        return "serverError";
    }

    private string _0003_CheckToPISSchema(string? loginName)
    {
        var schema = "secPis";
        var connName = "MySqlConn";

        LoginOutputModel model = new LoginOutputModel();
        HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Login/0001/EmpmasByEmpnumber/" + loginName + "/" + schema + "/" + connName).Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response?.Content.ReadAsStringAsync().Result;
            model = JsonConvert.DeserializeObject<LoginOutputModel?>(data);
            if (model != null)
            {
                return "exist";
            }
            return "notExist";
        }
        return "serverError";
    }

    // ---------- VERIFY EMPLOYEE NUMBER -----------------
    [AllowAnonymous]
    [HttpGet("login/VerifyUser")]
    public IActionResult VerifyUser(LoginInputModel input)
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("login/VerifyUserAndContinue")]
    public async Task<IActionResult> VerifyUserAndContinue(VerifyUserInputModel input, LoginOutputModel output)
    {
        string empNumber  = _empNumber;
        DateTime? dateHired  = input.DateHired;
        string secLicense = input.SecLicense;
        string movNumber = input.MovNumber;
        string schema = "secpis";

        LoginOutputModel model = new LoginOutputModel();
        HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Login/1004/validateUserFromEmpmas/" + empNumber + "/" + dateHired + "/" + secLicense + "/" + movNumber + "/" + schema).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            model = JsonConvert.DeserializeObject<LoginOutputModel?>(data);
            return Ok(model);
            if (model != null)
            {

            }
        }

                //Validate Employee in empmas
                //if (_0004_ValidateUsersOtherCredentialsInEmpmas(EmpNumber, DateHired, SecLicense, MovNumber, Schema) == "exist")
                //{
                //    return Ok(output);
                //    //Valid Credential
                //    //if(_0005_SaveCredentialsToMain())
                //}

                return Ok("error");

    }

    private string _0004_ValidateUsersOtherCredentialsInEmpmas(string empNumber, DateTime? dateHired, string secLicense, string movNumber, string schema)
    {
        LoginOutputModel model = new LoginOutputModel();
        HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Login/1004/validateUserFromEmpmas/" + empNumber + "/" + dateHired + "/" + secLicense + "/" + movNumber + "/" + schema).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            model = JsonConvert.DeserializeObject<LoginOutputModel?>(data);

            if (model != null)
            {
                return "exist";
            }
            return "notExist";
        }
        return "serverError";

}

    // ---------- Signin with Google --------------------
    public async Task<IActionResult> SigninWithGoogle()
    {
        var claims = User.Claims;
        var emailIdentifier = ClaimTypes.Email;
        var nameIdentifier = ClaimTypes.Name;

        var email = claims.FirstOrDefault(c => c.Type == emailIdentifier).Value;
        var name = claims.FirstOrDefault(c => c.Type == nameIdentifier).Value;

        var output = await _login.FetchEmployeeByEmailMain(email);
        if (output != null)
        {
            return Ok(output);
        }

        return Redirect("/register");
    }

}
