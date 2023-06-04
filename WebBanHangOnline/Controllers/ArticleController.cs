using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index(string alias)
        {
            var item = _db.Posts.FirstOrDefault(x => x.Alias == alias);
            return View(item);
        }
    }
}