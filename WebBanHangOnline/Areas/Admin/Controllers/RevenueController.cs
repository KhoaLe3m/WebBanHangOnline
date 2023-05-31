using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RevenueController : Controller
    {
        // GET: Admin/Revenue
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RevenueProfit(string fromDate, string toDate)
        {
            var query = from o in _db.Orders
                        join od in _db.OrderDetails
                        on o.Id equals od.Id
                        join pro in _db.Products
                        on od.ProductId equals pro.Id
                        select new
                        {
                            CreateDate = o.CreateAt,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = pro.OriginalPrice
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "DD/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "DD/MM/yyyy", null);
                query = query.Where(x => x.CreateDate < endDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreateDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y => y.Quantity * y.Price)
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.TotalSell,
                LoiNhuan = x.TotalSell - x.TotalBuy
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}