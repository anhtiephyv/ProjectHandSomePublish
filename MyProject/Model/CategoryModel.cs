using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models;
namespace MyProject.Model
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public int? ParentCategory { get; set; }
        public int CategoryLevel { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        public string ParentName { get; set; }
        public virtual Category CategoryParent { set; get; }
        public virtual IEnumerable<Product> Products { set; get; }
    }
}
