using LibraryMySql.Models; 
using LibraryMySql.DataAccess.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryMySql;
using Dapper;
using MySqlX.XDevAPI;
using GSIA.Models;

namespace GSIA.Controllers;

public class LoginController : Controller
{

    private readonly IConfiguration _configuration;
    private readonly ILoginAccess _login;
    private readonly IMySqlDataAccess _mysql;


    public LoginController(IConfiguration configuration, ILoginAccess login, IMySqlDataAccess mysql)
    {
        _configuration  = configuration;
        _login          = login;
        _mysql          = mysql;

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
        var output = await _login.LoginEmployee(input);
        
        if(output is not null)
        {
            if(output.Password.Length == 0)
            {
                ViewBag.EmpNumber = output?.EmpNumber;
                //return Redirect("login/Validation");
                return View("~/Views/Login/Validation.cshtml"); // Problem with redirecting the page using view
            }
            return View("~/Views/Main/Index.cshtml");
        }

        return View();
      
    }

    // --------- VALIDATION PAGE -----------

    [AllowAnonymous]
    [HttpGet("login/Validation")]
    public IActionResult Validation()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("login/Validation")]
    public  async Task<IActionResult> ValidateOtherCredentials(ValidationInputModel input)
    {
        string schema = input.Schema;
        var parameter = new DynamicParameters();
        parameter.Add("@Empnumber", input.EmpNumber, System.Data.DbType.String);
        parameter.Add("@Position_", input.Position_, System.Data.DbType.String);
        parameter.Add("@SecLicense", input.SecLicense, System.Data.DbType.String);
        parameter.Add("@MovNumber", input.MovNumber, System.Data.DbType.String);
        parameter.Add("@DateHired", input.DateHired, System.Data.DbType.DateTime);
        string sql = @" select  e.Empnumber, e.EmpLastNm, e.EmpFirstNm, e.EmpMidNm,
                                c.clNumber, c.ClName,
                                s.code EmpStatCd, s.Name EmpStatus, s.IsResigned,  
                                Position_ PositionCd, p.Name as Position,
                                e.DateHired,
                                e.Sss,
                                e.Tin,
                                e.SecLicense License,
                                e.MovNumber,
                                e.Email,
                                e.passwd
                            from " + schema + ".empmas e " +
                        " left join " + schema + ".client c on c.ClNumber = e.Client_ " +
                        " left join " + schema + ".empstat s on s.code = e.empstat_ " +
                        " left join " + schema + ".Position p on p.Code = e.Position_" +
                        " where e.Empnumber = @Empnumber and Position_ = @Position_ and e.SecLicense = @SecLicense and e.MovNumber = @MovNumber and e.DateHired = @DateHired ";

        var data = await _mysql.FetchData<LoginOutputModel?, dynamic>(sql, new {parameter });
        var output =  data.FirstOrDefault();

        if(output is not null)
        {
            //Save Password in database
            var par = new DynamicParameters();
            par.Add("@Password", input.Password, System.Data.DbType.AnsiString);
            string slq_insert = @"INSERT INTO " + schema + ".empmas e (passwd)" +
                                    "values (HASHBYTES('SHA2_512', @Password));";

     
            //await slq_insert.ExecuteCmdQS(sql, parameter, "Default");

        }

        return View();
    }
}
