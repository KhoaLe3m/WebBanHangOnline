using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            var items = _db.Products.ToList();
            return View(items);
        }
        public ActionResult Detail(string alias,int id)
        {
            var item = _db.Products.Find(id);
            if (item != null)
            {
                _db.Products.Attach(item);
                item.ViewCount += 1;
                _db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                _db.SaveChanges();
                return View(item);
            }
            return RedirectToAction("Error", "Home");
        }
        public ActionResult ProductCategory(string alias,int? id)
        {
            var items = _db.Products.ToList();
            if (id > 0 )
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            ViewBag.productCategoryId = id;
            var category = _db.ProductCategories.Find(id);
            if (category != null)
            {
                ViewBag.CategoryName = category.Title;
            }
            return View(items);
        }
        public ActionResult PartialItemByCategory()
        {
            var items = _db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView("_PartialItemByCategory", items);
        }
        public ActionResult PartialProductsSale()
        {
            var items = _db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView("_PartialProductsSale", items);
        }
    }
}