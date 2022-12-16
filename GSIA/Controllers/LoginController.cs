using GSIA.Models.Login;
using GsiaLibrary.DataAccess.Login;
using GsiaLibrary.Models;
using GsiaLibrary.Models.UI.Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;

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
    public async Task<IActionResult> Index()
    {
        // To clear claims ----------------
        await HttpContext.SignOutAsync();

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
    [HttpGet("register")]
    public IActionResult Register()
    {
        var claimsCount = User.Claims.Count();
        string hasClaims = "false";
        if (claimsCount != 0)
        {
            // WITH CLAIMS -----------------------------------------------
            hasClaims = "true";
        }

        ViewData["hasClaims"] = hasClaims;
        return View();
    }



    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAccount(RegisterInputModel input)
    {

        var claimsCount = User.Claims.Count();
        string hasClaims = "false";

        if (claimsCount != 0)
        {
            // User Not signed in with Google -----------------------------------------------
            var claims = User.Claims;
            string emailIdentifier = ClaimTypes.Email;
            string email = claims.FirstOrDefault(c => c.Type == emailIdentifier).Value;

            input.Email = email;
            hasClaims = "true";
        } 

        var data = _login._3000_RegisterAccount(input);
        if (data.ErrorField == null)
        {

            if(claimsCount == 0)
            {
                //CONVERT OUTPUT TO JS DATA ----------------------------------------------------------
                LoginOutputModel outputModel = new();
                outputModel = JsonConvert.DeserializeObject<LoginOutputModel>(data.QueryResult);

                //ADD NEW CLAIMS --------------------------------------------------------------------
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, outputModel.EmpFirstNm + outputModel.EmpLastNm ));
                claims.Add(new Claim(ClaimTypes.GivenName, outputModel.EmpFirstNm ));
                claims.Add(new Claim(ClaimTypes.Surname, outputModel.EmpLastNm));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimPrincipal);
            }
            return Redirect("/Main");
        }
        ViewData["hasClaims"] = hasClaims;
        ViewData["errorMessage"] = data.Description;
        return View("~/Views/Login/Register.cshtml");


    }

}
