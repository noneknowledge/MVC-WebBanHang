using Microsoft.AspNetCore.Mvc;
using MVC_template.Models;
using System.Diagnostics;

namespace MVC_template.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            GlobalValues.CustomerID = Guid.NewGuid().ToString();
        }

        public IActionResult Index()
        {
            return View("Show");
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