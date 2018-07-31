﻿using System;
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
       
        public string ProductDescription { get; set; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public int? ProductStatus { set; get; }
     //   public float ProductTags { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category ProductCategory { set; get; }
        public virtual ICollection<Tag> Tag { get; set; }
        public virtual ICollection<Sale> Sale { get; set; }
    }
}
