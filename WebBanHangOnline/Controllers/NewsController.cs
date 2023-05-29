using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = _db.News.ToList();
            return View(items);
        }
        public ActionResult Detail(int id)
        {
            var item = _db.News.Find(id);
            return View(item);
        }
        public ActionResult Partial_News_Home()
        {
            var items = _db.News.Take(3).ToList();
            return PartialView(items);
        }
    }
}