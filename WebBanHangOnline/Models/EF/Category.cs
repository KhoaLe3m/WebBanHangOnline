using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tbl_Category")]
    public class Category :CommonClass
    {
        public Category()
        {
            this.News = new HashSet<New>();
            this.Posts = new HashSet<Post>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [StringLength(150)]
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        public string Title { set; get; }
        [StringLength(150)]
        public string Alias { set; get; }
        public string Link { set; get; }
        public string Description { set; get; }
        public int Position { set; get; }
        public string SeoTitle { set; get; }
        public string SeoDescription { set; get; }
        public string SeoKeyWords { set; get; }
        public bool IsActive { set; get; }

        public ICollection<New> News { set; get; }
        public ICollection<Post> Posts { set; get; }
        public object Categories { get; internal set; }
    }
}