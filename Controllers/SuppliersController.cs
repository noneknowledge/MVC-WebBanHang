using Microsoft.AspNetCore.Mvc;
using MVC_template.Data;
using System.Reflection.Metadata.Ecma335;

namespace MVC_template.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly QLWebBanHangContext _context;

        public SuppliersController(QLWebBanHangContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Suppliers;
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Supplier model)
        {
            
            model.SupplierId = Guid.NewGuid().ToString();
            _context.Add(model);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            var data = _context.Suppliers.FirstOrDefault(a=>a.SupplierId == id);
            if (data != null)
            {
                _context.Remove(data); _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
