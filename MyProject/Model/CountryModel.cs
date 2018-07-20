using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Models;
namespace MyProject.Model
{
    public class CountryModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public byte[] CountryFlag { get; set; }
        public string CountryCronyms { get; set; }
        public string FileUploadName { get; set; }
        public string FileUploadType { get; set; }
        public byte[] FileUpload { get; set; }
        public int CountryStatus { get; set; }
        public int? NumberLine { get; set; }
        public DateTime LastUpdate { get; set; }
        public string DateDiff
        {
            get
            {
                var DatediffDateTime = DateTime.Now - LastUpdate;
                var StringResult = string.Empty;
                if (DatediffDateTime.Days != 0)
                {
                    StringResult += DatediffDateTime.Days + " ngày, ";
                }
                if (DatediffDateTime.Hours != 0)
                {
                    StringResult += DatediffDateTime.Hours + " giờ, ";
                }
                if (DatediffDateTime.Minutes != 0)
                {
                    StringResult += DatediffDateTime.Minutes + " phút, ";
                }
               
                    StringResult += DatediffDateTime.Seconds + " giây";
                

                return StringResult;
            }
        }
        public virtual IEnumerable<UserCountry> UserCountry { set; get; }
    }
}
