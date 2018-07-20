using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyProject.Base;
using Service.Service;
using AutoMapper;
using Data.Models;
using MyProject.helper;
using MyProject.Model;
using MyProject.Mapping;
using Data.Repository;
using AutoMapper.QueryableExtensions;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
namespace MyProject.Api
{
    [RoutePrefix("api/Users")]
    [Authorize]
    public class UsersController : BaseController
    {
        private IUsersService _Users;
        private IUserCountryService _UserCountry;
        public UsersController(IUsersService UserService, IUserCountryService UserCountry)
        {
            _Users = UserService;
            _UserCountry = UserCountry;
        }
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyword, int page, int pageSize, string orderby, string sortDir, string filter = null)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _Users.GetMultiPaging(x => x.FirstName.Contains(keyword) || x.LastName.Contains(keyword) || x.UserName.Contains(keyword) || string.IsNullOrEmpty(keyword) || x.Phone.Contains(keyword) || x.Email.Contains(keyword) || x.Address.Contains(keyword) || x.LastName.Contains(keyword) || x.UserName.Contains(keyword), out totalRow, orderby, sortDir, page, pageSize, null);
                IEnumerable<UsersModel> modelVm = Mapper.Map<IEnumerable<Users>, IEnumerable<UsersModel>>(model);

                PaginationSet<UsersModel> pagedSet = new PaginationSet<UsersModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, UsersModel userVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var modelVm = Mapper.Map<UsersModel, Users>(userVm);

                    _Users.Create(modelVm);
                    _Users.Save();

                    var responseData = Mapper.Map<Users, UsersModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("checkcodeExistEdit")]
        [HttpGet]
        public HttpResponseMessage checkcodeExistEdit(HttpRequestMessage request, string UserName, int? Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var responseData = _Users.checkCodeExist(UserName, Id);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);
                return response;
            });
        }
        [Route("checkcodeExist")]
        [HttpGet]
        public HttpResponseMessage checkcodeExist(HttpRequestMessage request, string UserName)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var responseData = _Users.checkCodeExist(UserName, null);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);
                return response;
            });
        }
        [Route("detail/{id}")]
        [HttpGet]
        //[Authorize(Roles = "ViewUser")]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            var UserVm = _Users.GetByID(id);
            var responseData = Mapper.Map<Users, UsersModel>(UserVm);
            response = request.CreateResponse(HttpStatusCode.Created, responseData);
            return response;


        }
        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, UsersModel userVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var modelVm = Mapper.Map<UsersModel, Users>(userVm);

                    _Users.Update(modelVm);
                    _Users.Save();

                    var responseData = Mapper.Map<Users, UsersModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            _Users.Delete(id);
            _Users.Save();

            return request.CreateResponse(HttpStatusCode.OK, id);
        }
        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _Users.Delete(item);
                    }

                    _Users.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
        [Route("createUserCountry")]
        [HttpPost]
        public HttpResponseMessage CreateUserCountry(HttpRequestMessage request, int UserID, List<SelectListModel> selectlistVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    List<UserCountry> listUserCountry = selectlistVm.Select(c => new UserCountry { UserID = UserID, CountryID = Convert.ToInt32(c.id) }).ToList();

                    _UserCountry.CreateUserCountry(listUserCountry);
                    _UserCountry.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, 0);
                }

                return response;
            });
        }
    }
}
