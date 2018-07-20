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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        //[StringLength(50)]
        //public string ProductSeri { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategory { get; set; }
        public int CategoryLevel { get; set; }
        public string CountryCronyms { get; set; }
        public int CountryStatus { get; set; }
        public int? NumberLine { get; set; }
        public DateTime LastUpdate { get; set; }
        public virtual IEnumerable<UserCountry> UserCountry { set; get; }
    }
}
