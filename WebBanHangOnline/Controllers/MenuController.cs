using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = _db.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop",items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = _db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory");
        }
        public ActionResult MenuLeft(int? id)
        {
            if(id != null)
            {
                ViewBag.productCategoryId = id;
            }
            var items = _db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult MenuArrivals()
        {
            var items = _db.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }
    }
}