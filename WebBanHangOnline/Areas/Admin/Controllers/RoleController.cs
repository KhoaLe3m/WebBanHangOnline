using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Admin/Role
        public ActionResult Index()
        {
            var item = _db.Roles.ToList();
            return View(item);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                roleManager.Create(model);
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var item = _db.Roles.Find(id);
            if (item != null)
            {
                return View(item);
            }
            return RedirectToAction("Index", "Role");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                roleManager.Update(model);
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }
    }
}