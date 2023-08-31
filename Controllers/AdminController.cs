using Microsoft.AspNetCore.Mvc;
using MVC_template.Data;
using MVC_template.Models;
using System.Linq;

namespace MVC_template.Controllers
{
    public class AdminController : Controller
    {
        private readonly QLWebBanHangContext _context;

        public AdminController(QLWebBanHangContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            if (GlobalValues.VaiTro == "admin")
            {
                DateTime today = DateTime.Now;
                var data = _context.Orders
                    .Where(order => order.OrderDate == today)
                    .Select(g => new DoanhThuNgay
                    {
                        OrderId = g.OrderId,
                        AmountPaid = g.AmountPaid
                    }).ToList();
                return View(data);
            }
            else {
                return NotFound("Không tìm thấy trang web.");
            }
        }
        public IActionResult IndexJson()
        {
                DateTime today = DateTime.Now;
                var data = _context.Orders
                    .Where(order => order.OrderDate == today)
                    .Select(g => new DoanhThuNgay
                    {
                        OrderId = g.OrderId,
                        AmountPaid = g.AmountPaid
                    }).ToList();
                return Json(data);
        }


        public IActionResult DoanhThuToday()// View Index
        {
            DateTime today = DateTime.Now;
            var data = _context.OrderDetails
                .Where(order => order.Order.OrderDate == today)
                .Select(g => new DoanhThuNgay
                {
                    OrderId = g.OrderId,
                    AmountPaid = g.Order.AmountPaid
                }).ToList();
            return Json(data);
        }
        #region Sản phẩm đã bán
        public ActionResult DoanhThuTotal()// Tổng số lượng đã bán
        {
            if (GlobalValues.VaiTro == "admin")
            {
                var data = _context.OrderDetails.GroupBy(order => new
                    {
                        order.Product.ProductName
                    })
                        .Select(g => new productSold
                        {
                            ProductName = g.Key.ProductName,
                            Quantity = g.Sum(ct => ct.Quantity),

                        }).ToList();
                return View(data);
            }
            else
            {
                return NotFound("Không tìm thấy trang web.");
            }
            

            

        }
        public ActionResult DoanhThuTotalChart()
        {
            var data = _context.OrderDetails.GroupBy(order => new
            {
                order.Product.ProductName
            })
                .Select(g => new productSold
                {
                    ProductName = g.Key.ProductName,
                    Quantity = g.Sum(ct => ct.Quantity),

                }).ToList();

            return Json(data);

        }
        #endregion Sản phẩm đã bán
        #region TheoNgay
        public ActionResult DoanhThuTheoNgayJson(DateTime Day)
        {
            
            var data = _context.Orders
                .Where(order => order.OrderDate == Day)
                .Select(g => new DoanhThuTuan
                {
                    OrderId = g.OrderId,
                    AmountPaid = g.AmountPaid
                }).ToList();
            return PartialView("_Layout1",data);
        }
        public ActionResult DoanhThuTheoNgay()
        {
            if (GlobalValues.VaiTro == "admin")
            {
                return View();
            }
            else
            {
                return NotFound("Không tìm thấy trang web.");
            }
            
        }
        #endregion TheoNgay
        #region Theo Tuần
        public ActionResult DoanhThuTheoTuan()
        {
            if (GlobalValues.VaiTro == "admin")
            {
                DateTime today = DateTime.Now;
                // Lấy ngày đầu tiên của tuần (thứ hai)
                DateTime firstDay = today.AddDays(-(int)today.DayOfWeek + 1);
                // Lấy ngày cuối cùng của tuần (chủ nhật)
                DateTime lastDay = today.AddDays(7 - (int)today.DayOfWeek);
                // In ra kết quả
                //var firstDay= firstDay.ToString("dd/MM/yyyy"));
                //lastDay.ToString("dd/MM/yyyy"));
                var data = _context.Orders
                    .Where(order => order.OrderDate <= lastDay && order.OrderDate >= firstDay)
                    .Select(g => new DoanhThuTuan
                    {
                        OrderId = g.OrderId,
                        AmountPaid = g.AmountPaid
                    }).ToList();
                return View(data);
            }
            else
            {
                return NotFound("Không tìm thấy trang web.");
            }
            
        }
        public ActionResult DoanhThuTuanChart()
        {
            DateTime today = DateTime.Now;
            // Lấy ngày đầu tiên của tuần (thứ hai)
            DateTime firstDay = today.AddDays(-(int)today.DayOfWeek + 1);
            // Lấy ngày cuối cùng của tuần (chủ nhật)
            DateTime lastDay = today.AddDays(7 - (int)today.DayOfWeek);
            // In ra kết quả
            //var firstDay= firstDay.ToString("dd/MM/yyyy"));
            //lastDay.ToString("dd/MM/yyyy"));
            var data = _context.Orders
                .Where(order => order.OrderDate <= lastDay && order.OrderDate >= firstDay)
                .Select(g => new DoanhThuTuan
                {
                    OrderId = g.OrderId,
                    AmountPaid = g.AmountPaid
                }).ToList();
            return Json(data);
        }
        #endregion Theo Tuần
        #region Suppliers
        public ActionResult SuppliersDoanhThu()
        {
            if (GlobalValues.VaiTro == "admin")
            {
                var data = _context.Products.GroupBy(sp => new
                    {
                        sp.Supplier.SupplierName
                    })
                        .Select(g => new SuppliersDoanhThu
                        {
                            SupplierName = g.Key.SupplierName,
                            AmountPaid = g.Sum(s => s.Prices * s.Quantity)



                        }).ToList();
                return View(data);
        }
            else
            {
                return NotFound("Không tìm thấy trang web.");
            }
            
        }
        public ActionResult SuppliersDoanhThuChart()
        {

            var data = _context.Products.GroupBy(sp => new
            {
                sp.Supplier.SupplierName
            })
                .Select(g => new SuppliersDoanhThu
                {

                    AmountPaid = g.Sum(s => s.Prices * s.Quantity),
                    SupplierName = g.Key.SupplierName

                }).ToList();
            return Json(data);
        }
#endregion  Suppliers
        public ActionResult DoanhThuThang()
        {
            DateTime today = DateTime.Now;
            int month = today.Month;
            int year = today.Year;
            int days = DateTime.DaysInMonth(year, month);
            DateTime firstDay = new DateTime(year, month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
            DateTime weekStart = firstDay;
            DateTime weekEnd = weekStart.AddDays(6);
            List<DoanhThuTuanTotal> doanhThuTuan = new List<DoanhThuTuanTotal>();
            for (int i = 1; i <= days; i++)
            {
                // Lấy ngày hiện tại
                DateTime currentDay = new DateTime(year, month, i);
                // Kiểm tra xem ngày hiện tại có phải là ngày đầu tiên của tuần hay không
                if (currentDay.DayOfWeek == DayOfWeek.Monday)
                {
                    // Nếu có, cập nhật lại ngày đầu tiên và cuối cùng của tuần
                    weekStart = currentDay;
                    weekEnd = weekStart.AddDays(6);
                }
                // Kiểm tra xem ngày hiện tại có phải là ngày cuối cùng của tuần hay không
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || currentDay == lastDay)
                {
                    var totalAmountPaid = _context.OrderDetails
                        .Where(order => order.Order.OrderDate >= weekStart && order.Order.OrderDate <= currentDay)
                        .Sum(order => order.Order.AmountPaid);
                    // Tạo một đối tượng DoanhThuTuan với số tuần, ngày đầu tiên, ngày cuối cùng và tổng số tiền thanh toán
                    DoanhThuTuanTotal doanhThu = new DoanhThuTuanTotal
                    {
                        WeekNumber = i / 7 + 1,
                        WeekStart = weekStart,
                        WeekEnd = currentDay,
                        TotalAmountPaid = (int)(totalAmountPaid ?? 0)
                    };
                    // Thêm đối tượng này vào danh sách doanhThuTuan
                    doanhThuTuan.Add(doanhThu);
                }
            }
            doanhthuthang doanhthu = new doanhthuthang();
            doanhthu.DoanhThuTuan = doanhThuTuan;
            // Trả về đối tượng danhthuthang cho view
            return View(doanhthu);

        }

    }
}
