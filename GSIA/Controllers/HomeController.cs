﻿using GSIA.Models;
using LibraryMySql.DataAccess.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GSIA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ILoginAccess data )
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return Redirect("/login");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}