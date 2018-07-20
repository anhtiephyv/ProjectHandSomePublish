using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyProject.Base;
using MyProject.Model;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Data.Models;
using Data.DBContext;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Text;

namespace MyProject.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseController
    {


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string userName, string password, bool rememberMe = false)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(userName, password, rememberMe, shouldLockout: false);
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[HttpPost]
        //[Authorize]
        //[Route("logout")]
        //public HttpResponseMessage Logout(HttpRequestMessage request)
        //{
        //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        //    authenticationManager.SignOut();
        //    return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("Login")]
        //public async Task<HttpResponseMessage> Login(AccountModel model)
        //{
        //    // Invoke the "token" OWIN service to perform the login: /api/token
        //    // Ugly hack: I use a server-side HTTP POST because I cannot directly invoke the service (it is deeply hidden in the OAuthAuthorizationServerHandler class)
        //    var request = HttpContext.Current.Request;
        //    var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/api/Token";
        //    using (var client = new HttpClient())
        //    {
        //        var requestParams = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("grant_type", "password"),
        //        new KeyValuePair<string, string>("username", model.UserName),
        //        new KeyValuePair<string, string>("password", model.Password)
        //    };
        //        var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
        //        var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
        //        var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
        //        var responseCode = tokenServiceResponse.StatusCode;
        //        var responseMsg = new HttpResponseMessage(responseCode)
        //        {
        //            Content = new StringContent(responseString, Encoding.UTF8, "application/json")
        //        };
        //        return responseMsg;
        //    }
        //}
        //[Route("api/logout")]
        //public HttpResponseMessage Logout(HttpRequestMessage request)
        //{
        //    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        //    authenticationManager.SignOut();
        //    return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        //}
        // Hàm đăng ký
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new MyShopDBContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };
            IdentityResult result = manager.Create(user, model.Password);
            return result;
        }
        // Hàm tạo user claims
        [HttpGet]
        [Route("api/GetUserClaims")]
        [Authorize]
        public AccountModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            AccountModel model = new AccountModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
            return model;
        }
    }
}
