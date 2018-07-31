using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Models
{
    [Table("Product")]
    public class ProductItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductItemID { get; set; }
        [MaxLength(100)]
        public int ProductID { get; set; }

        public int Quantity { get; set; }
        public string Colors { get; set; }
        [Column(TypeName = "xml")]
        public string Size { get; set; }
        public float Price { get; set; }

     //   public float ProductTags { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
    }
}
