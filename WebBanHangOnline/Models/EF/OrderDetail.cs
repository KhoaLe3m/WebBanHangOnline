using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tbl_OrderDetail")]
    public class OrderDetail
    {
        [key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public decimal Price { set; get; }
        public int Quantity { set; get; }
        public virtual Order Orders { set; get; }
        public virtual Product Products { set; get; }
    }
}