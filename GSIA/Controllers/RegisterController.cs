using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSIA.Controllers
{
    public class RegisterController : Controller
    {
        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Index()
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
}
