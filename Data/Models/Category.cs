using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            CategoryChildren = new HashSet<Category>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public int? ParentCategoryID { get; set; }
        public int CategoryLevel { get; set; }
        public int? DisplayOrder  { get; set; }
        public string Description { get; set; }
        [ForeignKey("ParentCategoryID")]
        public virtual Category CategoryParent { set; get; }
        public virtual ICollection<Product> Products { set; get; }
        public virtual ICollection<Category> CategoryChildren { set; get; }
    }
}
