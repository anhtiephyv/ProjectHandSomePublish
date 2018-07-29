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
        public int ProductQuantity { get; set; }
        //     public int? BrandID { get; set; }
        public string ProductColor { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
     //   public float ProductTags { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { set; get; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
