using Microsoft.AspNetCore.Mvc;

namespace GSIA.Controllers.Sample
{
    public class PatrialViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
