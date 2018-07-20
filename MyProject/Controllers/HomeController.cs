using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Service.Service;
using AutoMapper;
using MyProject.Model;
using Data.Models;
using System.Collections.Generic;
namespace MyProject.Controllers
{


    public class HomeController : Controller
    {
        private ICountryService _countryService;
        public HomeController(ICountryService CountryService)
        {
            _countryService = CountryService;
        }
        public ActionResult Index()
        {
            //  var check = _adminService.GetAll().ToList();
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = System.Web.HttpContext.Current.User.Identity.Name;
            var check = _countryService.getCountrybyUserName(System.Web.HttpContext.Current.User.Identity.Name);
            IEnumerable<CountryModel> modelVm = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryModel>>(check);
            return View(modelVm);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Header()
        {
            return PartialView();
        }
        public ActionResult Footer()
        {
            return PartialView();
        }
        public ActionResult Banner()
        {
            return PartialView();
        }

    }
}