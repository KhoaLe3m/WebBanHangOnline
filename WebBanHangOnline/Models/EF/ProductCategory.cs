using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tbl_ProductCategory")]
    public class ProductCategory : CommonClass
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        [StringLength(150)]
        public string Title { set; get; }
        
        [StringLength(150)]
        public string Alias { set; get; }
        public string Description { set; get; }
/*        public string Icon { set; get; }
*/        [StringLength(500)]
        public string SeoTitle { set; get; }
        [StringLength(500)]
        public string SeoDescription { set; get; }
        [StringLength(500)]
        public string SeoKeyWords { set; get; }
        public ICollection<Product> Products { set; get; }
    }
}