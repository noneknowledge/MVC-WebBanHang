using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC_template.Data;
using MVC_template.Models;
using NuGet.Packaging.Signing;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
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
        public IActionResult Home()
        {
            
            var homeData = new HomeProduct();
            var newProducts = _context.Products.Include(a=>a.Supplier).Where(a=>a.Quantity>1 && a.IsHide=="false").OrderByDescending(a => a.Stt).Take(4);
            var topProducts  = _context.OrderDetails.GroupBy(a => a.ProductId).OrderByDescending(a => a.Sum(p => p.Quantity)).Select(a=>a.Key).ToList();
            var topSaleProducts  = _context.Products.Include(a=>a.Supplier).Where(a=>topProducts.Contains(a.ProductId) && a.Quantity > 1 && a.IsHide == "false").Take(4);

            homeData.topSaleProducts = topSaleProducts;
            homeData.newProducts = newProducts;
            return View(homeData);
        }

        public Boolean CheckQuantity(string productID, int quantity)
        {
            var product = _context.Products.FirstOrDefault(a=>a.ProductId== productID);
            if (product == null)
            { return false; }
            if (product.Quantity < quantity) { return false; }
            return true;
        }

        public IActionResult Index()
        {
            var supplier = _context.Suppliers.ToList();
            ViewBag.Supplier = supplier;
            var data = _context.Products.Include(a => a.Supplier).Where(a => a.Quantity > 1 && a.IsHide == "false").ToList();
            return View(data);
        }

        [HttpGet("Shopping/Index/NhaPhanPhoi/{idSupplier}")]
        public IActionResult IndexOrderBySupplier(string idSupplier)
        {
            
            ViewBag.SupplierId = idSupplier;
            var supplier = _context.Suppliers.ToList();
            ViewBag.Supplier = supplier;
            var data = _context.Products.Include(a => a.Supplier).Where(a=> a.Quantity > 1 && a.IsHide == "false" && a.SupplierId == idSupplier).ToList();
            return View("Index",data);
        }

        [HttpGet("Shopping/Index/{orValue}")]
        public IActionResult IndexOrderByPrice(string orValue)
        {
            var supplier = _context.Suppliers.ToList();
            ViewBag.Supplier = supplier;
            if (orValue == "tang")
            {
                var data = _context.Products.Where(a => a.Quantity > 1 && a.IsHide == "false").Include(a=>a.Supplier).OrderBy(a => a.Prices);
                return View("Index", data);
            }
            else
            {
                var data = _context.Products.Where(a => a.Quantity > 1 && a.IsHide == "false").Include(a => a.Supplier).OrderByDescending(a => a.Prices);
                return View("Index", data);
            }
            
        }
        [HttpGet("Shopping/Index/NhaPhanPhoi/{idSupplier}/{orValue}")]
        public IActionResult IndexOrderBySupplierPrice(string idSupplier, string orValue)
        {
            var supplier = _context.Suppliers.ToList();
            ViewBag.SupplierId = idSupplier;
            ViewBag.Supplier = supplier;
            if (orValue=="tang")
            {
                var data = _context.Products.Where(a=> a.Quantity > 1 && a.IsHide == "false" && a.SupplierId == idSupplier).Include(a => a.Supplier).OrderBy(a=>a.Prices).ToList();
                return View("Index", data);

            }
            else
            {
                var data = _context.Products.Where(a => a.Quantity > 1 && a.IsHide == "false" && a.SupplierId == idSupplier).Include(a => a.Supplier).OrderByDescending(a => a.Prices).ToList();
                return View("Index", data);
            }
        }

       
        [HttpGet]
        public IActionResult Login() 
        { 

            return View();
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            var userlogin = _context.UserLogins.Include(a=>a.Customer).FirstOrDefault(a=>a.UserName== model.UserName && a.PassWord == model.PassWord);
            if (userlogin==null)
            {
                ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu";
                return View();
            }
            if (userlogin.VaiTro == "customer")
            {
                GlobalValues.CustomerID = userlogin.CustomerId;
                GlobalValues.Name = userlogin.Customer.LastName + " " + userlogin.Customer.FirstName;
                GlobalValues.VaiTro = userlogin.VaiTro;
                return RedirectToAction("Home", "Shopping");
            }
            else
            {
                GlobalValues.VaiTro = userlogin.VaiTro;
                return RedirectToAction("Index", "Products");
            }
            
            

           
        }

        public IActionResult Logout() 
        {
            GlobalValues.logOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]        
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterVM account)
        {
            string strRegex = @"0[3789][0-9]{8}";
            Regex re = new Regex(strRegex);
            int a;
            if (account.LastName== null || account.UserName == null || account.FirstName == null || account.Address == null || account.PassWord ==null)
            {
                ViewBag.Validate = "Không được để trống trường nào";
                return View();
                
            }
            else if (!int.TryParse(account.PhoneNumber,out a))
            {
                ViewBag.Validate = "Số điện thoại phải là số";
                return View();
            }
            else if (account.PhoneNumber.Length >11 || account.PhoneNumber.Length <10)
            {
                ViewBag.Validate = "Số điện thoại tối đa là 11 số và tối thiểu là 10 số";
                return View();
            } 
           
            
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

            ViewBag.Validate = "Tạo tài khoản thành công";
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string keyword)
        {
            var data = _context.Products.Include(a=>a.Supplier).Where(a=>a.ProductName.Contains(keyword)).ToList();

            return PartialView("PartialViewProduct",data);
        }

        [HttpPost]
        public IActionResult AdvancedSearch(string keyword, int? fromValue, int? toValue)
        {
            if (fromValue == null)
            {
                fromValue = 0;
            }
            if(toValue == null)
            {
                toValue = 999999999;
            }
            if (keyword == null)
            {
                var data = _context.Products.Include(a=>a.Supplier).Where(a=> a.Quantity > 1 && a.IsHide == "false" && a.Prices >= fromValue && a.Prices <= toValue).ToList();
                return PartialView("PartialViewProduct", data);
            }
            else
            {
                var data = _context.Products.Include(a => a.Supplier).Where(a => a.Quantity > 1 && a.IsHide == "false" && a.ProductName.Contains(keyword) && a.Prices >= fromValue && a.Prices <= toValue).ToList();
                return PartialView("PartialViewProduct", data);
            }
            
          
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
            if (GlobalValues.CustomerID== null) 
            {
               
                return RedirectToAction("Login");
            }

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
            if (GlobalValues.CustomerID == null)
            {
                return RedirectToAction("Login", "Shopping");
            }    
            var customerInfor = _context.Customers.FirstOrDefault(a=>a.CustomerId== GlobalValues.CustomerID);
            
            var data = _context.ShoppingCarts.Include(a=>a.Product).Where(a=>a.CustomerId == GlobalValues.CustomerID).ToList();
            foreach (var item in data)
            {
                if (CheckQuantity(item.ProductId,item.Quantity.Value) == false)
                {
                    TempData["alert"] = "Sản phẩm: " + item.Product.ProductName + " đã hết số lượng bạn cần";
                    return RedirectToAction(nameof(Cart));
                }
            }
            if (data.Count() == 0)
            {
                TempData["alert"] = "Trong giỏ chưa có sản phẩm nào";
                return RedirectToAction(nameof(Cart));
            }    
                

            var order = new Order();
            order.OrderId = Guid.NewGuid().ToString();
            order.OrderDate = DateTime.Now.Date;
            order.CustomerId = GlobalValues.CustomerID;
            order.Address = customerInfor.Address;
            order.Phone = customerInfor.PhoneNumber;
            _context.Add(order);
            _context.SaveChanges();
            foreach (var item in data)
            {
                var productQuantity = _context.Products.FirstOrDefault(a => a.ProductId == item.ProductId);
                var OrderDetail = new OrderDetail();
                OrderDetail.Quantity = item.Quantity;
                OrderDetail.Total = item.Total;
                OrderDetail.Unit= item.Unit;
                OrderDetail.ProductId= item.ProductId;
                OrderDetail.OrderId = order.OrderId;

                productQuantity.Quantity -= item.Quantity;
                _context.Update(productQuantity);

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
