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

        public IActionResult Login() 
        { 

            return View();
        }

        [HttpGet]        
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterVM account)
        {
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid().ToString(),
                FirstName = account.FirstName,
                LastName = account.LastName,    
                PhoneNumber= account.PhoneNumber,
                Address = account.Address,
            };

            _context.Add(customer);
            _context.SaveChanges();


            var userlogin = new UserLogin
            {
                UserName= account.UserName,
                PassWord= account.PassWord,
                VaiTro = "customer",
                CustomerId = customer.CustomerId,
            };
            _context.Add(userlogin);
            _context.SaveChanges();


            return View();
        }

        public IActionResult Delete(string id)
        {
            var product = _context.ShoppingCarts.FirstOrDefault(a => a.ProductId == id && a.CustomerId == GlobalValues.CustomerID);
            if (product == null)
            {
                return View("Error");
            }
            _context.ShoppingCarts.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet("/Product/{id}")]
        public IActionResult ProductDetail(string id)
        {
            var data = _context.Products.Where(a=>a.ProductId== id);
            if (data.Count() == 0)
            {
                ViewBag.ChiTiet = "Không tìm thấy sản phẩm";
                return View(data);
            }

            return View(data);
        }

        public IActionResult AddToCart(string id,int price,int quantity=1)
        {

            var cartItem = _context.ShoppingCarts.SingleOrDefault(a => a.ProductId == id && a.CustomerId == GlobalValues.CustomerID);
            
            if (cartItem == null)
            {
                var newItem = new ShoppingCart();
                newItem.ProductId = id;
                newItem.CustomerId = GlobalValues.CustomerID;
                newItem.Quantity = quantity;
                newItem.Unit = price;
                newItem.Total = price * newItem.Quantity;
                _context.Add(newItem);
                _context.SaveChanges();
            }
            else
            {
                cartItem.Quantity = quantity;
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
            
            if (data.Count() == 0) 
            {
                ViewBag.Cart = "Chưa có sẳn phẩm nào trong giỏ hàng";
                ViewBag.ButtonBuy = "Mua ngay";
            }
            
            return View(data);
        }

        

        public IActionResult AddToOrder()
        {
           
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

                _context.Remove(item);
                _context.SaveChanges();
                _context.Add(OrderDetail);
                _context.SaveChanges();
            }
            order.AmountPaid = data.Sum(a=>a.Total); 
            
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
       
        public IActionResult OrderDetail(string id,int ordertotal)
        {
            var data = _context.OrderDetails.Include(a=>a.Product).Include(a=>a.Order).Where(a => a.OrderId == id).ToList();
            var orderInfo = data.FirstOrDefault(a => a.OrderId != null);
            ViewBag.OrderDate = orderInfo.Order.OrderDate.Value.ToLongDateString();
            ViewBag.OrderId = id;
            ViewBag.TotalOrder = ordertotal;
            return View(data);
        }
        

    }
}
