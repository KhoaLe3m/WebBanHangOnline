using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tbl_Order")]
    public class Order:CommonClass
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public string Code { set; get; }
        [Required(ErrorMessage ="Tên khách hàng không được để trống")]
        public string CustomerName { set; get; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(15,ErrorMessage ="Độ dài số điện thoại không được quá 15 số")]
        public string Phone { set; get; }
        [Required(ErrorMessage ="Địa chỉ không được để trống")]
        public string Address { set; get; }
        public string Email { set; get; }
        public decimal TotalAmount { set; get; }
        [Required]
        public int Status { set; get; }
        public int TypeOfPayment { set; get; }
        public int Quantity { set; get; }
        public ICollection<OrderDetail> OrderDetails { set; get; }

    }
}