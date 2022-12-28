using Microsoft.AspNetCore.Mvc;
using MysqlApiLibrary.Models;
using MysqlMvc.Models.Login;

namespace MysqlMvc.Controllers; 

public class LoginController : Controller
{
    private readonly IHttpClientFactory _factory;

    public LoginController(IHttpClientFactory factory)
    {
        _factory = factory;
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult UserLogin()
    {
        return View();
    }

    //private readonly LoginOutputModel login = new(); 

    //[HttpPost("UserLogin1")]
    //public async void UserLogin(LoginInputModel user)
    /*{
        var client = _factory.CreateClient("api");
        var userInfo = await client.PostAsJsonAsync<LoginOutputModel>("", login); 
        
        //return RedirectToAction("UserLogin");
        
        //return View();
    }
    */



}
