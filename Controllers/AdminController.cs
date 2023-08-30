using Microsoft.AspNetCore.Mvc;

namespace MVC_template.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
