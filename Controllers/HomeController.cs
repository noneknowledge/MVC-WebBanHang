using Microsoft.AspNetCore.Mvc;
using MVC_template.Data;
using MVC_template.Models;
using System.Diagnostics;

namespace MVC_template.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly QLWebBanHangContext _context;
        
        public HomeController(ILogger<HomeController> logger,QLWebBanHangContext context)
        {
            _logger = logger;
            _context= context;
            
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