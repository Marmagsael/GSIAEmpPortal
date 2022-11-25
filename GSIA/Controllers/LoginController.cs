using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSIA.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        //[HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult ValidateEmployee()
        {
            return View();
        }
    }
}
