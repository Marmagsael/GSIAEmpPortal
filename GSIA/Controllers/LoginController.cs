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

namespace GSIA.Controllers;

public class LoginController : Controller
{

    private readonly IConfiguration _configuration;
    private readonly ILoginAccess _login;
    private readonly IMySqlDataAccess _mysql;
    private static string _empNumber = "";

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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> ValidateInitialCredential(LoginInputModel? input)
    {    
        if (input?.EmpNumber != null)
        {
            //Find Employee Number In Main
            input.Schema = "main";
            var output =  await _login.LoginEmployeeByEmpnoMain(input);
            if (output != null)
            {
                //Check if input Password matches the saved password
                if(input.Password != null){
                    if (input.Password == output.Password)
                    {
                        return View("~/Views/Main/Index.cshtml");
                    }
                    else
                    {
                        ViewData["errPassMsg"] = "Incorrect password";
                        return View("~/Views/login/Index.cshtml");
                    }
                } else
                {
                    ViewData["errPassMsg"] = "Please enter your password";
                    return View("~/Views/login/Index.cshtml");
                }
                
            } else
            {
                //Find Employee Number In Secpis
                input.Schema = "secpis";
                output = await _login.LoginEmployee(input);
                if (output != null)
                {
                    ViewData["empno"] = input.EmpNumber;
                    _empNumber = input.EmpNumber;
                    return View("~/Views/login/VerifyUser.cshtml");
                }
            }
            
            
        }
        return Ok("Please provide your employee number and password");
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
    public  async Task<IActionResult> VerifyUserAndContinue(VerifyUserInputModel input)
    {
        input.Schema = "secpis";
        input.EmpNumber = _empNumber;
        var output = await _login.LoginEmployee(input.Schema, input.EmpNumber);
        if(output != null)
        {
            if(input.SecLicense == output.License  && 
                input.DateHired == output.DateHired &&
                input.MovNumber == output.MovNumber) {

                var parameter = new DynamicParameters();
                parameter.Add("@Empnumber", input.EmpNumber, System.Data.DbType.String);
                parameter.Add("@EmpLastNm", output.EmpLastNm, System.Data.DbType.String);
                parameter.Add("@EmpFirstNm", output.EmpFirstNm, System.Data.DbType.String);
                parameter.Add("@EmpMidNm", output.EmpMidNm, System.Data.DbType.String);
                parameter.Add("@Clnumber", output.Clnumber, System.Data.DbType.String);
                parameter.Add("@ClName", output.ClName, System.Data.DbType.String);
                parameter.Add("@EmpStatCd", output.EmpStatCd, System.Data.DbType.String);
                parameter.Add("@EmpStatus", output.EmpStatus, System.Data.DbType.String);
                parameter.Add("@IsResigned", output.IsResigned, System.Data.DbType.String);
                parameter.Add("@PositionCd", output.PositionCd, System.Data.DbType.String);
                parameter.Add("@Position", output.Position, System.Data.DbType.String);
                parameter.Add("@DateHired", output.DateHired, System.Data.DbType.DateTime);
                parameter.Add("@Sss", output.Sss, System.Data.DbType.String);
                parameter.Add("@Tin", output.Tin, System.Data.DbType.String);
                parameter.Add("@License", output.License, System.Data.DbType.String);
                parameter.Add("@MovNumber", output.MovNumber, System.Data.DbType.String);
                parameter.Add("@Email", output.Email, System.Data.DbType.String);
                parameter.Add("@Password", input.Password, System.Data.DbType.AnsiString);

                //Save Data in Main Database
                input.Schema = "main";
                var sql = "Insert into " + input.Schema + ".users (EmpNumber, EmpLastNm, EmpFirstNm, EmpMidNm, Clnumber," +
                            "ClName, EmpStatCd, EmpStatus, IsResigned, PositionCd, Position, DateHired, Sss, " +
                            "Tin, License, MovNumber, Email, Password) " +
                            "Values (@EmpNumber, @EmpLastNm, @EmpFirstNm, @EmpMidNm, @Clnumber, " +
                            "@ClName, @EmpStatCd, @EmpStatus, @IsResigned, @PositionCd, @Position, @DateHired, @Sss," +
                            "@Tin, @License, @MovNumber, @Email,  @Password)";
               // HASHBYTES('SHA2_512', @Password)
                await _mysql.ExecuteCmd(sql,parameter);

                return Redirect("/Main");
            } else {
                return Ok("Not Match!");
            }
        }
        return Ok("error");

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
        if(output != null)
        {
            return Ok(output);
        }

        return Redirect("/register");
    }

}
