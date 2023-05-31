using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingSystemController : Controller
    {
        // GET: Admin/SettingSystem
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_Setting()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            // Title
            var checkTitle = _db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null||string.IsNullOrEmpty(checkTitle.SettingValue))
            {
                SystemSetting item = new SystemSetting();
                item.SettingKey = "SettingTitle";
                item.SettingValue = req.SettingTitle;
                _db.SystemSettings.Add(item);
                _db.SaveChanges();

            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                _db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

            }
            // Logo
            var checkLogo = _db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                SystemSetting item = new SystemSetting();
                item.SettingKey = "SettingLogo";
                item.SettingValue = req.SettingLogo;
                _db.SystemSettings.Add(item);
                _db.SaveChanges();

            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                _db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            // Hotline
            var checkHotline = _db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHoline"));
            if (checkHotline == null)
            {
                SystemSetting item = new SystemSetting();
                item.SettingKey = "SettingHoline";
                item.SettingValue = req.SettingHoline;
                _db.SystemSettings.Add(item);
                _db.SaveChanges();
            }
            else
            {
                checkHotline.SettingValue = req.SettingHoline;
                _db.Entry(checkHotline).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

            }
            // Email
            var checkEmail = _db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (checkEmail == null)
            {
                SystemSetting item = new SystemSetting();
                item.SettingKey = "SettingEmail";
                item.SettingValue = req.SettingEmail;
                _db.SystemSettings.Add(item);
                _db.SaveChanges();

            }
            else
            {
                checkEmail.SettingValue = req.SettingEmail;
                _db.Entry(checkEmail).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            // TittleSEO
            var checkTittleSEO = _db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (checkTittleSEO == null)
            {
                SystemSetting item = new SystemSetting();
                item.SettingKey = "SettingTitleSeo";
                item.SettingValue = req.SettingTitleSeo;
                _db.SystemSettings.Add(item);
                _db.SaveChanges();
            }
            else
            {
                checkTittleSEO.SettingValue = req.SettingTitleSeo;
                _db.Entry(checkTittleSEO).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            // DescSEO
            var checkDescSEO = _db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDescSeo"));
            if (checkDescSEO == null)
            {
                SystemSetting item = new SystemSetting();
                item.SettingKey = "SettingDescSeo";
                item.SettingValue = req.SettingDescSeo;
                _db.SystemSettings.Add(item);
                _db.SaveChanges();
            }
            else
            {
                checkDescSEO.SettingValue = req.SettingDescSeo;
                _db.Entry(checkDescSEO).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            // KeySEO
            var checkKeySEO = _db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (checkKeySEO == null)
            {
                SystemSetting item = new SystemSetting();
                item.SettingKey = "SettingKeySeo";
                item.SettingValue = req.SettingKeySeo;
                _db.SystemSettings.Add(item);
                _db.SaveChanges();
            }
            else
            {
                checkKeySEO.SettingValue = req.SettingKeySeo;
                _db.Entry(checkKeySEO).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
            return View("Partial_Setting");
        }
    }
}