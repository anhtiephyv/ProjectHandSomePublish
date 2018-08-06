using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [MaxLength(100)]
        public string ProductName { get; set; }

        public int CategoryID { get; set; }

        public string MetaDescription { get; set; }
        public bool? HomeFlag { set; get; }
        public string Alias { set; get; }
        public bool? HotFlag { set; get; }
        public int ViewCount { set; get; }
        public int? ProductStatus { set; get; }
        public string MetaKeyword { set; get; }
        [MaxLength(256)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }
        [ForeignKey("CategoryID")]
        public virtual Category ProductCategory { set; get; }
    }
}
