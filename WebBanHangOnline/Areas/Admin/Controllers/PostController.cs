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

    public class PostController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Admin/New
        public ActionResult Index()
        {
            var items = _db.Posts.ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreateAt = DateTime.Now;
                model.ModifiderDate = DateTime.Now;
                model.CategoryId = 3;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                _db.Posts.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _db.Posts.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                _db.Posts.Attach(model);
                model.ModifiderDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _db.Posts.Find(id);
            if (item != null)
            {
                _db.Posts.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false }); ;

        }
        [HttpPost]
        public ActionResult Active(int id)
        {
            var item = _db.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive });
            }
            return Json(new { success = false, IsActive = item.IsActive }); ;

        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var arrayId = ids.Split(',');
                if (arrayId != null && arrayId.Any())
                {
                    foreach (var id in arrayId)
                    {
                        var item = _db.Posts.Find(Int32.Parse(id));
                        _db.Posts.Remove(item);
                        _db.SaveChanges();
                    }
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}