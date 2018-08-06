using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Models
{
    [Table("ProductTag")]
    public class ProductTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTagID { get; set; }
        public int ProductID { get; set; }
        public int TagID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
        [ForeignKey("TagID")]
        public virtual Tag Tag { set; get; }
      
    }
}
