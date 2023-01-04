using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSIA.Controllers
{
    public class ProfilesController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _111PersonnelInformation()
        {
            return View();
        }

        public IActionResult _112Employment()
        {
            return View();
        }

        public IActionResult _113Trainings()
        {
            return View();
        }

        public IActionResult _114Uploadables()
        {
            return View();
        }

        public IActionResult _115CorrectionRequest()
        {
            return View();
        }
    }
}
