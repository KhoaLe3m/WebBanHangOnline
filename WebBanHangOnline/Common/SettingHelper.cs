using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHangOnline.Models;

namespace WebBanHangOnline
{
    public static class SettingHelper
    {
        private static ApplicationDbContext _db = new ApplicationDbContext();
        public static string GetValue(string key)
        {
            var item = _db.SystemSettings.Find(key);
            if(item != null)
            {
                return item.SettingValue;
            }
            return "";
        }
    }
}