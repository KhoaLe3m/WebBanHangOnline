using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class CustomerViewModel
    {
        public string CustomerName { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public int PaymentMethod { set; get; }

    }
}