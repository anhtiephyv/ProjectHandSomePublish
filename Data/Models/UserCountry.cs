using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Models
{
    [Table("UserCountry")]
    public class UserCountry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserCountryID { get; set; }
        public int UserID { get; set; }
        public int CountryID { get; set; }
        [ForeignKey("UserID")]
        public virtual Users User { set; get; }
        [ForeignKey("CountryID")]
        public virtual Country Country { set; get; }
    }
}
