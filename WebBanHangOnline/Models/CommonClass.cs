using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public abstract class CommonClass
    {
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifiderDate { get; set; }
        public string ModifiderBy { get; set; }

    }
}