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
using GsiaLibrary.DataAccess.General;
using System.Security.Principal;
using GsiaLibrary.Models.UI._001Main;
using GsiaLibrary.DataAccess._100Main;

namespace GSIA.Controllers;

public class LoginController : Controller
{
    private readonly IMenuData _menu;
    private readonly ILoginData _login;
    private readonly ICompanyData _company;

    public LoginController(ILoginData login, ICompanyData company, IMenuData menu)
    {
        _login = login;
        _company = company;
        _menu = menu;
    }



    [AllowAnonymous]
    [HttpGet("login")]
    public async Task<IActionResult> Index()
    {
        ViewData["CoName"] = _company.GetCompanyInfo();
        return View();
    }

    // --------- LOGIN PAGE -----------
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> ValidateEmployeeByLoginNameAndPassword(LoginInputModel input)
    {

        string coName = "";
        var data = _login._10000_ValidateEmployeeByLoginNameAndPassword(input);


        //If ERROR OCCURED, DISPLAY ERROR MESSAGE
        if (data.ErrorField is not null)
        {
            // -- DISPLAY SERVER ERROR MESSAGE IN LOGIN PAGE  
            if (data.ErrorField == "Server")
            {
                ViewData["coName"] = coName;
                ViewData["errServerMsg"] = data.Description;
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
        //CONVERT OUTPUT TO JS DATA ----------------------------------------------------------
        LoginOutputModel outputModel = new();
        outputModel = JsonConvert.DeserializeObject<LoginOutputModel>(data.QueryResult!)!;

        //ADD NEW CLAIMS --------------------------------------------------------------------
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, outputModel.EmpFirstNm + ' ' + outputModel.EmpLastNm));
        claims.Add(new Claim(ClaimTypes.GivenName, outputModel.EmpFirstNm));
        claims.Add(new Claim(ClaimTypes.Surname, outputModel.EmpLastNm));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync(claimPrincipal);

        // -- Get Menu -----------------------------------------------------------------------
       
        return Redirect("/Profiles/_111PersonnelInformation");
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
        return Redirect("/Profiles/_111PersonnelInformation");
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

        var data = await _login._3000_RegisterAccount(input);
        if (data.ErrorField == null)
        {

            if (claimsCount == 0)
            {
                //CONVERT OUTPUT TO JS DATA ----------------------------------------------------------
                LoginOutputModel outputModel = new();
                outputModel = JsonConvert.DeserializeObject<LoginOutputModel>(data.QueryResult);

                //ADD NEW CLAIMS --------------------------------------------------------------------
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, outputModel.EmpFirstNm + outputModel.EmpLastNm));
                claims.Add(new Claim(ClaimTypes.GivenName, outputModel.EmpFirstNm));
                claims.Add(new Claim(ClaimTypes.Surname, outputModel.EmpLastNm));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimPrincipal);
            }
            return Redirect("/Pis");
        }
        ViewData["hasClaims"] = hasClaims;
        ViewData["errorMessage"] = data.Description;
        return View("~/Views/Login/Register.cshtml");
    }

    public async Task<IActionResult> Logout()
    {
        // CLEAR CLAIMS ----------------
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index");
    }
}
