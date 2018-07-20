using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Service;
namespace MyProject.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
               //private IAppUsersService _usersService;
        private ICountryService _Country;
        // private ICountryRepository _CountryRepository;
        public FileController(ICountryService CountryService)
        {
            _Country = CountryService;
            //   _CountryRepository = CountryRepository;
        }
        public FileResult Download(int id)
        {
            var countryVm = _Country.GetByID(id);

            return File(countryVm.FileUpload, System.Net.Mime.MediaTypeNames.Application.Octet, countryVm.FileUploadName);
        }
	}
}