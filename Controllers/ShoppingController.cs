using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_template.Data;
using MVC_template.Models;

namespace MVC_template.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly QLWebBanHangContext _context;
        public ShoppingController(QLWebBanHangContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Products.Include(a => a.Supplier).ToList();
            return View(data);
        }
        public IActionResult AddToCart(string id,int price)
        {

            var cartItem = _context.ShoppingCarts.SingleOrDefault(a => a.ProductId == id && a.CustomerId == GlobalValues.CustomerID);
            
            if (cartItem == null)
            {
                var newItem = new ShoppingCart();
                newItem.ProductId = id;
                newItem.CustomerId = GlobalValues.CustomerID;
                newItem.Quantity = 1;
                newItem.Unit = price;
                _context.Add(newItem);
                _context.SaveChanges();
            }
            else
            {
                cartItem.Quantity += 1;
                _context.Update(cartItem);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
