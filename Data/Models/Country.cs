using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Models
{
    [Table("Country")]
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }
        //[StringLength(50)]
        //public string ProductSeri { get; set; }
        public string CountryName { get; set; }
        public byte[] CountryFlag { get; set; }
        public byte[] FileUpload { get; set; }
        public string FileUploadName { get; set; }
        public string FileUploadType { get; set; }
        public string CountryCronyms { get; set; }
        public int CountryStatus { get; set; }
        public int? NumberLine { get; set; }
        public DateTime LastUpdate { get; set; }
        public virtual IEnumerable<UserCountry> UserCountry { set; get; }
    }
}
