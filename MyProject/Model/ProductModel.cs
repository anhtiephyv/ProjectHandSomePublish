using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models;
namespace MyProject.Model
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        //     public int? BrandID { get; set; }

        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string Images { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int ViewCount { set; get; }
        public string Image { set; get; }
        public string MoreImages { set; get; }
        public string Alias { set; get; }
        public int? ProductStatus { set; get; }
        public List<TagModel> Tags { get; set; }
    }
}
