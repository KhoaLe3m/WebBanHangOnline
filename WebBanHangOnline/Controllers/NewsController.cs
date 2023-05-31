using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<New> items = _db.News.OrderByDescending(x => x.Id);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items.ToPagedList(pageIndex, pageSize));
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