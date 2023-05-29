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

    public class ProductImageController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items = _db.ProductImages.Where(x => x.ProductId == id).ToList();
            return View(items);
        }
        [HttpPost]
        public ActionResult AddImage(int productId,string url)
        {
            _db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            _db.SaveChanges();
            return Json(new {  Success = true} );
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _db.ProductImages.Find(id);
            if (item != null)
            {
                _db.ProductImages.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false }); ;

        }
    }
}