using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportLogger.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is a test web site to showcase ASP.NET Core technologies.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Alan Sidney Brown";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
