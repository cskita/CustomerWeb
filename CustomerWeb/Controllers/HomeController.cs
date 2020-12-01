using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomerWeb.Models;
using CustomerWeb.Extensions;

namespace CustomerWeb.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            var isAthenticated = HttpContext.Session.IsAuthenticated();

            ViewData["LoggedIn"] = isAthenticated;

            if (!isAthenticated)
                return View("./Login");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
