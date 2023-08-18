using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_template.Data;
using MVC_template.Models;
using System.Xml.Schema;

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
                newItem.Total = price * newItem.Quantity;
                _context.Add(newItem);
                _context.SaveChanges();
            }
            else
            {
                cartItem.Quantity += 1;
                cartItem.Total = price * cartItem.Quantity;
                _context.Update(cartItem);
                _context.SaveChanges();
            }
            
            TempData["alert"] = "Đã thêm vào giỏ hàng";

            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            var data = _context.ShoppingCarts.Include(a => a.Product).Where(a=>a.CustomerId== GlobalValues.CustomerID).ToList();
            int? total = 0;
            if (data.Count() == 0) 
            {
                ViewBag.Cart = "Chưa có sẳn phẩm nào trong giỏ hàng";
                ViewBag.ButtonBuy = "Mua ngay";
            }
            else
            {
                
                foreach (var item in data)
                {
                   total += item.Total;
                }
            }
            ViewBag.Total = total;
            return View(data);
        }
        public IActionResult AddToOrder()
        {
            int? OrderTotal = 0;
            var order = new Order();
            order.OrderId = Guid.NewGuid().ToString();  
            order.OrderDate = DateTime.Now.Date;
            order.CustomerId = GlobalValues.CustomerID;
            _context.Add(order);
            _context.SaveChanges();
            var data = _context.ShoppingCarts.Where(a=>a.CustomerId == GlobalValues.CustomerID).ToList();
            foreach (var item in data)
            {
                var OrderDetail = new OrderDetail();
                OrderDetail.Quantity = item.Quantity;
                OrderDetail.Total = item.Total;
                OrderDetail.Unit= item.Unit;
                OrderDetail.ProductId= item.ProductId;
                OrderDetail.OrderId = order.OrderId;
                OrderTotal += item.Total;

                _context.Remove(item);
                _context.SaveChanges();
                _context.Add(OrderDetail);
                _context.SaveChanges();
            }
            order.AmountPaid = OrderTotal;
            
            
            
            
            _context.Update(order);
            _context.SaveChanges();


            TempData["alert"] = "Đã đặt hàng";
            return RedirectToAction("Cart");
        }


        public IActionResult Order()
        {
            var data = _context.Orders.Where(a => a.CustomerId == GlobalValues.CustomerID).ToList();
            return View(data);
        }
       
        public IActionResult OrderDetail(string id)
        {
            var data = _context.OrderDetails.Where(a => a.OrderId == id).ToList();
            return View(data);
        }
        

    }
}
