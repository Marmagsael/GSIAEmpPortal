using Dapper;
using LibraryMySql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSIA.Controllers
{
    public class RegisterController : Controller
    {
        [AllowAnonymous]
        [HttpGet("register")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("register/RegisterAccount")]
        public IActionResult RegisterAccount(RegisterInputModel input)
        {
            return Ok();
        }
    }
}
