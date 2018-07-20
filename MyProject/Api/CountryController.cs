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
    [RoutePrefix("api/Country")]
    [Authorize]
    public class CountryController : BaseController
    {
        private ICountryService _Country;
        private IUserCountryService _UserCountry;
        public CountryController(ICountryService CountryService,IUserCountryService UserCountryService)
        {
            _Country = CountryService;
            _UserCountry = UserCountryService;
        }
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyword, int page, int pageSize, string orderby, string sortDir, string filter = null)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _Country.GetMultiPaging(x => x.CountryName.Contains(keyword) || x.FileUploadName.Contains(keyword) || x.CountryCronyms.Contains(keyword) || string.IsNullOrEmpty(keyword), out totalRow, orderby, sortDir, page, pageSize, null);
                IEnumerable<CountryModel> modelVm = Mapper.Map<IEnumerable<Country>, IEnumerable<CountryModel>>(model);

                PaginationSet<CountryModel> pagedSet = new PaginationSet<CountryModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, CountryModel countryVm)
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
                    var modelVm = Mapper.Map<CountryModel, Country>(countryVm);
                    modelVm.LastUpdate = DateTime.Now;
                    _Country.Create(modelVm);
                    _Country.Save();

                    var responseData = Mapper.Map<Country, CountryModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [Route("checkcodeExistEdit")]
        [HttpGet]
        public HttpResponseMessage checkcodeExistEdit(HttpRequestMessage request, string CountryCode, int? Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var responseData = _Country.checkCodeExist(CountryCode, Id);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("checkcodeExist")]
        [HttpGet]
        public HttpResponseMessage checkcodeExist(HttpRequestMessage request, string CountryCode)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var responseData = _Country.checkCodeExist(CountryCode, null);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("detail/{id}")]
        [HttpGet]
        //[Authorize(Roles = "ViewUser")]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            var countryVm = _Country.GetByID(id);
            var responseData = Mapper.Map<Country, CountryModel>(countryVm);
            response = request.CreateResponse(HttpStatusCode.OK, responseData);
            return response;


        }
        //[Route("getFile/{id}")]
        //[HttpGet]
        //public HttpResponseMessage GetFile(int id)
        //{
        //    //string fileName;
        //    var countryVm = _Country.GetByID(id);


        //        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

        //        result.Content = new ByteArrayContent(countryVm.FileUpload); ;
        //        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //        result.Content.Headers.ContentDisposition.FileName = countryVm.FileUploadName;
        //        return result;

        //   // return new HttpResponseMessage(HttpStatusCode.NotFound);
        //}
        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, CountryModel countryVm)
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
                    var modelVm = Mapper.Map<CountryModel, Country>(countryVm);
                    modelVm.LastUpdate = DateTime.Now;
                    _Country.Update(modelVm);
                    _Country.Save();

                    var responseData = Mapper.Map<Country, CountryModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            _Country.Delete(id);
            _Country.Save();
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
                        _Country.Delete(item);
                    }

                    _Country.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
        [Route("getlist")]
        [HttpGet]
        public HttpResponseMessage GetList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var countryVm = _Country.Get(x => x.CountryStatus == 1).OrderBy(x => x.CountryName).Select(c => new SelectListModel { id = c.CountryID.ToString(), name = c.CountryName + " - " + c.CountryFlag }).ToArray();

                response = request.CreateResponse(HttpStatusCode.OK, countryVm);
                return response;
            });
        }
        [Route("GetSelectedCountry")]
        [HttpGet]
        public HttpResponseMessage GetSelectedCountry(HttpRequestMessage request, int userID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
               var listSelectedCountry = _UserCountry.GetUserCountryByUserID(userID).Select(x=> x.CountryID).ToList();
               var countryVm = _Country.Get(x => x.CountryStatus == 1 && listSelectedCountry.Contains(x.CountryID)).OrderBy(x => x.CountryName).Select(c => new SelectListModel { id = c.CountryID.ToString(), name = c.CountryName + " - " + c.CountryCronyms }).ToArray();

                response = request.CreateResponse(HttpStatusCode.OK, countryVm);
                return response;
            });
        }
        [Route("GetUnSelectedCountry")]
        [HttpGet]
        public HttpResponseMessage GetUnSelectedCountry(HttpRequestMessage request, int userID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var listSelectedCountry = _UserCountry.GetUserCountryByUserID(userID).Select(x => x.CountryID).ToList();
                var countryVm = _Country.Get(x => x.CountryStatus == 1 && !listSelectedCountry.Contains(x.CountryID)).OrderBy(x => x.CountryName).Select(c => new SelectListModel { id = c.CountryID.ToString(), name = c.CountryName + " - " + c.CountryCronyms }).ToArray();

                response = request.CreateResponse(HttpStatusCode.OK, countryVm);
                return response;
            });
        }
        [Route("CreateUserCountry")]
        [HttpPost]
        public HttpResponseMessage CreateUserCountry(HttpRequestMessage request, int userID, List<SelectListModel> selectedselectedItems)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var ListCountryID = _UserCountry.GetUserCountryByUserID(userID);
                var ListDeleteUserCountry = ListCountryID.Where(x => !selectedselectedItems.Select(c=> Convert.ToInt32(c.id)).Contains(x.CountryID)).ToList();
                var ListCreateUserCountryID = selectedselectedItems.Where(x => !ListCountryID.Select(c=> c.CountryID.ToString()).Contains(x.id)).ToList();
                _UserCountry.DelelteUserCountry(ListDeleteUserCountry);
                var ListCreateUserCountry = ListCreateUserCountryID.Select(c => new UserCountry { UserID = userID, CountryID = Convert.ToInt32(c.id) }).ToList();
                _UserCountry.CreateUserCountry(ListCreateUserCountry);
                _UserCountry.Save();
                response = request.CreateResponse(HttpStatusCode.Created, 0);
                return response;
            });
        }
    }
}
