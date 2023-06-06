using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]

    public class OrderController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Order> items = _db.Orders.OrderByDescending(x => x.Id);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult View(int id)
        {
            var item = _db.Orders.Find(id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction("Index", "Order");
        }
        public ActionResult PartialProductView(int id) {
            var items = _db.OrderDetails.Where(x => x.OrderId == id);
            if (items != null)
            {
                return PartialView(items);
            }
            return RedirectToAction("Index", "Post");
        }
        [HttpPost]
        public ActionResult UpdateTT(int id,int trangthai)
        {
            var code = new { Success = false, code = -1 };
            var item = _db.Orders.Find(id);
            if (item != null)
            {
                _db.Orders.Attach(item);
                item.TypeOfPayment = trangthai;
                _db.Entry(item).Property(x => x.TypeOfPayment).IsModified = true;
                _db.SaveChanges();
                code = new { Success = true, code = 1 };
                return Json(code);
            }
            return Json(code);

                

        }
    }
}