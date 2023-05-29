using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangOnline.Models.EF
{
    public class Product:CommonClass
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int ProductCategoryId { set; get; }
        public string ProductCode { set; get; }
        [Required]
        [StringLength(150)]
        public string Title { set; get; }
        [StringLength(150)]
        public string Alias { set; get; }
        public string Description { set; get; }
        [AllowHtml]
        public string Detail { set; get; }
        public string Image { set; get; }
        public decimal Price { set; get; }
        public decimal? PriceSale { set; get; }
        public int Quantity { set; get; }
        public bool IsHome { set; get; }
        public bool IsHot { set; get; }
        public bool IsSale { set; get; }
        public bool IsActive { set; get; }
        public int ViewCount { set; get; }
        public bool IsFeature { set; get; }
        [StringLength(500)]
        public string SeoTitle { set; get; }
        [StringLength(500)]
        public string SeoDescription { set; get; }
        [StringLength(500)]
        public string SeoKeyWords { set; get; }
        public virtual ProductCategory ProductCategory { set; get; }
        public virtual ICollection<ProductImage> ProductImages { set; get; }
        public virtual ICollection<OrderDetail> OrderDetails { set; get; }
    }
}