using LibraryMySql.Models; 
using LibraryMySql.DataAccess.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryMySql;
using Dapper;
using MySqlX.XDevAPI;
using GSIA.Models;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace GSIA.Controllers;

public class LoginController : Controller
{

    private readonly IConfiguration _configuration;
    private readonly ILoginAccess _login;
    private readonly IMySqlDataAccess _mysql;
    private static string EmpNum = null;



    public LoginController(IConfiguration configuration, ILoginAccess login, IMySqlDataAccess mysql)
    {
        _configuration = configuration;
        _login = login;
        _mysql = mysql;

    }


    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Index()
    {
        return View();
    }

    // --------- LOGIN PAGE -----------
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> ValidateInitialCredentials(LoginInputModel input)
    {

        if (input.EmpNumber != null)
        {
            //Check if Employee Number Exists in Main Table
            input.Schema = "main";
            var EmpNoInMain = await _login.FetchEmployeeInMainByEmpNo(input);
            if (EmpNoInMain != null)
            {
                //Check Password
                if (input.Password != null)
                {
                    var userInMain = await _login.FetchEmployeeInMainByEmpNoAndPassword(input);
                    if (userInMain != null)
                    {
                        return View("~/Views/Main/Index.cshtml");
                    }
                    else
                    {
                        ViewData["errPasswordMsg"] = "Incorrect password";
                        return View("~/Views/login/Index.cshtml");
                    }
                }
                else
                {
                    ViewData["errPasswordMsg"] = "Enter your password";
                    return View("~/Views/login/Index.cshtml");
                }
            }
            else
            {
                //Otherwise check Employee Number in Secpis Table
                input.Schema = "secpis";
                var EmpNoInSecpis = await _login.LoginEmployee(input);
                if (EmpNoInSecpis != null)
                {
                    //Pass Employee Number then redirect to the account verification page
                    ViewData["empNo"] = input.EmpNumber;
                    //ValidationInputModel data= new ValidationInputModel();
                    //data.VEmpNumber = input.EmpNumber;

                    EmpNum = input.EmpNumber;


                    return View("~/Views/login/AccountVerification.cshtml");


                }
                else
                {
                    ViewData["errEmpNoMsg"] = "Employee number doesn't exist";
                    return View("~/Views/login/Index.cshtml");
                }
            }
        }

        return Ok("Please enter an employee number");

    }

    // --------- VALIDATION PAGE -----------

    [AllowAnonymous]
    [HttpGet("login/AccountVerification")]
    public IActionResult Validation()
    {
        return View();
    }



    [AllowAnonymous]
    [HttpPost("login/AccountVerification")]
    public async Task<IActionResult> ValidateOtherCredentials(ValidationInputModel input)
    {

        string schema = "secpis";

        var parameter = new DynamicParameters();
        parameter.Add("@Empnumber", EmpNum, System.Data.DbType.String);
        parameter.Add("@Position_", input.Position_, System.Data.DbType.String);
        parameter.Add("@SecLicense", input.SecLicense, System.Data.DbType.String);
        parameter.Add("@MovNumber", input.MovNumber, System.Data.DbType.String);
        parameter.Add("@DateHired", input.DateHired, System.Data.DbType.DateTime);
        string sql = @" select  e.Empnumber, e.EmpLastNm, e.EmpFirstNm, e.EmpMidNm,
                                e.DateHired,
                                e.SecLicense License,
                                e.MovNumber
                       from " + schema + ".empmas e " +
                            " where e.Empnumber = @Empnumber and e.SecLicense = @SecLicense and e.MovNumber = @MovNumber and e.DateHired = @DateHired ";

        var data = await _mysql.FetchData<LoginOutputModel?, dynamic>(sql, parameter);
        var output = data.FirstOrDefault();

        if (output != null)
        {
            //Save Data in Main db
            schema = "main";
            var parameter2 = new DynamicParameters();
            parameter2.Add("@Empnumber", output.EmpNumber, System.Data.DbType.String);
            parameter2.Add("@Position_", output.PositionCd, System.Data.DbType.String);
            parameter2.Add("@SecLicense", output.License, System.Data.DbType.String);
            parameter2.Add("@MovNumber", output.MovNumber, System.Data.DbType.String);
            parameter2.Add("@DateHired", output.DateHired, System.Data.DbType.DateTime);
            parameter.Add("@Password", input.VPassword, System.Data.DbType.AnsiString);

            var cmd = "Insert into " + schema + ".users  ( Empnumber,  License,  MovNumber,  DateHired)" +
                        "values ( @Empnumber, @SecLicense, @MovNumber, @DateHired)";
            await _mysql.ExecuteCmd(cmd, parameter2);


            //check if data is successfully saved
            //var userInMain = await _login.FetchEmployeeInMainByEmpNoAndPassword(output);

            //once saved
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, output.EmpFirstNm + output.EmpLastNm));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimPrincipal);

            return Redirect("/Main");
        }

        return Ok("Credentials are incorrect");
    }


    // --------- LOGIN WITH GOOGLE ----------
    public async Task<IActionResult> SignInWithGoogle()
    {
        var claims = User.Claims;
        var emailIdentifier = ClaimTypes.Email;
        //Check if email exist in main db


        //if emploee doesn't exist in main db redirect user to registration page
        // return Redirect("/login/Register");
        return View();
    }


    // -------- REGISTRATION PAGE -----------
    [AllowAnonymous]
    [HttpGet("login/Register")]
    public IActionResult Regsiter()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("login/Register")]
    public async Task<IActionResult> Regsiter(LoginInputModel input)
    {
        //Save Data in main db

        //Once Saved
        return Redirect("/Main");
    }
}
