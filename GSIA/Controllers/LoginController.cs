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

    public LoginController(ILoginData login)
    {
        _login = login;
    }



    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Index()
    {
        ViewData["CoName"] = _login.GetCompanyInfo();
        return View();
    }

    // --------- LOGIN PAGE -----------
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult ValidateEmployeeByLoginNameAndPassword(LoginInputModel input)
    {

        string coName = _login.GetCompanyInfo();
        var data = _login._10000_ValidateEmployeeByLoginNameAndPassword(input);

        //If ERROR OCCURED, DISPLAY ERROR MESSAGE
        if (data.ErrorField is not null)
        {
            // -- DISPLAY SERVER ERROR MESSAGE IN LOGIN PAGE  
            if (data.ErrorField == "Server")
            {
                ViewData["coName"] = coName;
                ViewData["errPasswordMsg"] = data.Description;
                return View("Index");
            }

            // -- DISPLAY PASSWORD ERROR MESSAGE IN LOGIN PAGE  
            if (data.ErrorField == "Password")
            {
                ViewData["coName"] = coName;
                ViewData["errPasswordMsg"] = data.Description;
                return View("Index");
            }

            // -- DISPLAY EMPLOYEE NUMBER ERROR MESSAGE IN LOGIN PAGE  
            if (data.ErrorField == "EmployeeNo")
            {
                ViewData["coName"] = coName;
                ViewData["errEmpNoMsg"] = data.Description;
                return View("Index");
            }

        }
        // -- IF NO ERROR OCCUR, REDIRECT USER TO LANDING PAGE 
        return Redirect("/Main");
    }


    // --------- SIGN IN WITH GOOGLE -----------
    public IActionResult SignInWithGoogle()
    {
        var claims = User.Claims;
        string emailIdentifier = ClaimTypes.Email;
        string email = claims.FirstOrDefault(c => c.Type == emailIdentifier).Value;

        var data = _login._20000_ValidateEmployeeByEmail(email);

        //CHECK IF EMAIL EXISTS IN MAIN TABLE
        if (data.QueryResult.Length == 0)
        {
            return RedirectToAction("Register");
        }
        return Redirect("/Main");

    }


    // --------- REGISTRATION PAGE -----------
    [AllowAnonymous]
    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View();
    }



    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult RegisterAccount()
    {
        return View();
    }

}
