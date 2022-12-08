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
