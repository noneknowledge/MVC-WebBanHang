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
        
        public IActionResult Details(string id)
        {
            var data = _context.Suppliers.FirstOrDefault(a => a.SupplierId == id);

            return View(data);
        }
        public IActionResult Edit(string id)
        {
            var data = _context.Suppliers.FirstOrDefault(a => a.SupplierId == id);

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Supplier model)
        {
            var Suppliers = _context.Suppliers.FirstOrDefault(a => a.SupplierId == model.SupplierId);
            Suppliers.SupplierName = model.SupplierName;
            Suppliers.Description = model.Description;  

            _context.Update(Suppliers);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
