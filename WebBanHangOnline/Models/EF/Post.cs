using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangOnline.Models.EF
{
    [Table("tbl_Post")]
    public class Post :CommonClass
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int CategoryId { set; get; }
        [Required]
        [StringLength(150)]
        public string Title { set; get; }
        [StringLength(150)]
        public string Alias { set; get; }

        public string Description { set; get; }
        [AllowHtml]
        public string Detail { set; get; }
        [StringLength(150)]
        public string Image { set; get; }
        [StringLength(500)]
        public string SeoTitle { set; get; }
        [StringLength(500)]
        public string SeoDescription { set; get; }
        [StringLength(500)]
        public string SeoKeyWords { set; get; }
        public bool IsActive { set; get; }

        public virtual Category Category { set; get; }

    }
}