using GsiaLibrary.DataAccess.Login;
using GsiaLibrary.Models;
using GsiaLibrary.Models.UI.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GSIA.Controllers;

public class LoginController : Controller
{
    private readonly ILoginData _login;
    private static string _empNumber;

    public LoginController(ILoginData login, IHttpClientFactory httpClientFactory)
    {
        _login = login;
    }



    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Index()
    {
        ViewData["CoName"]= _login.GetCompanyInfo();
        return View();
    }

    // --------- LOGIN PAGE -----------
    [AllowAnonymous]
    [HttpPost("login")]
    public  IActionResult ValidateEmployeeByLoginNameAndPassword(LoginInputModel input)
    {
        
        string coName = _login.GetCompanyInfo();
        var data = _login.ValidateEmployeeByLoginNameAndPassword(input);
        // -- DISPLAY SERVER ERROR MESSAGE IN LOGIN PAGE -- 
        if (data.ErrorField == "Server")
        {
            ViewData["coName"] = coName;
            ViewData["errPasswordMsg"] = data.Description;
            return View("Index");
        }

        // -- DISPLAY PASSWORD ERROR MESSAGE IN LOGIN PAGE -- 
        if (data.ErrorField == "Password")
        {
            ViewData["coName"] = coName;
            ViewData["errPasswordMsg"] = data.Description;
            return View("Index");
        }

        // -- DISPLAY EMPLOYEE NUMBER ERROR MESSAGE IN LOGIN PAGE -- 
        if (data.ErrorField == "EmployeeNo")
        {
            ViewData["coName"] = coName;
            ViewData["errEmpNoMsg"] = data.Description;
            return View("Index");
        }

        // -- REDIRECT USER TO OTHER PAGE IF NO ERROR OCCUR -- 
        if (data.ErrorField is null)
        {
            if(data.Description == "Password Match")
            {
                // -- REDIRECT USER IN LANDING PAGE IF EMPLOYEE NO EXIST IN MAIN TABLE --
                return Redirect("/Main");
            }
            if (data.Description == "Employee number exists in secpis")
            {
                // -- REDIRECT USER TO VERIFY ACCOUNT PAGE IF USER EXIST IN SECPIS TABLE --
                _empNumber = input.EmpNumber;
                ViewData["empNo"] = _empNumber;
                return View("VerifyAccount");
;            }
        }

      
        return Ok(data);
    }


    // --------- VERIFY ACCOUNT PAGE -----------
    [AllowAnonymous]
    [HttpGet("login/VerifyAccount")]
    public IActionResult VerifyAccount(string empno)
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("login/VerifyAccount")]
    public IActionResult ValidateEmployeeByAddedCredentials(VerifyAccountInputModel input)
    {

        input.vEmpNumber = _empNumber;
        var data = _login.ValidateEmployeeByAddedCredentials(input);

        if(data.ErrorField == "annonymous" || data.ErrorField == "server")
        {
            //CREDENTIALS DID NOT MATCH
            ViewData["errCredential"]   = data.Description;
            ViewData["empNo"]           = _empNumber;
            return View("VerifyAccount");
        } else
        {
            //CREDENTIALS MATCH
            return Redirect("/Main");
        }
    }


    // --------- SIGN IN WITH GOOGLE -----------
    public IActionResult SignInWithGoogle()
    {
        var claims              = User.Claims;
        string emailIdentifier  = ClaimTypes.Email;
        string email            = claims.FirstOrDefault(c => c.Type == emailIdentifier).Value;

        var data = _login.ValidateEmployeeByEmail(email);

        //CHECK IF EMAIL EXISTS IN MAIN TABLE
        if(data.QueryResult.Length == 0)
        {
            return Redirect("/Register");
        }
        return Redirect("/Main");
       
    }
}
