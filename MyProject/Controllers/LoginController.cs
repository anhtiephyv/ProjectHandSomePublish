using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Service;
using System.Web.Security;
using Newtonsoft.Json.Linq;
namespace MyProject.Controllers
{
    public class LoginController : Controller
    {
        private IUsersService _userService;
        public LoginController(IUsersService UserService)
        {
            _userService = UserService;
        }
        public bool Index(string jsonData)
        {
            var UserName = JObject.Parse(jsonData)["userName"].ToString();

            var user = _userService.Get(x => x.UserName == UserName).FirstOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);

                var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddSeconds(15), false, "Admin,Editor");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return true;
            }
            return false;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}