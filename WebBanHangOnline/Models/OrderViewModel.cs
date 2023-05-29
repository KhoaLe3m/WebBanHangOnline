using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string CustomerName { set; get; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(15, ErrorMessage = "Độ dài số điện thoại không được quá 15 số")]
        public string Phone { set; get; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { set; get; }
        public string Email { set; get; }
        public int TypeOfPayment { set; get; }
    }
}