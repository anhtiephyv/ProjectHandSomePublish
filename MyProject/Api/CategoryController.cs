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
    [RoutePrefix("api/Category")]
    [AllowAnonymous]
    public class CategoryController : BaseController
    {
        private ICategoryService _Category;
        public CategoryController(ICategoryService CategoryService)
        {
            _Category = CategoryService;
        }
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyword, int page, int pageSize, string orderby, string sortDir, string filter = null)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _Category.GetMultiPaging(x => x.CategoryName.Contains(keyword) || string.IsNullOrEmpty(keyword), out totalRow, orderby, sortDir, page, pageSize, new string[] { "CategoryParent" });
                var check = model.ToList();
                IEnumerable<CategoryModel> modelVm = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(model);

                PaginationSet<CategoryModel> pagedSet = new PaginationSet<CategoryModel>()
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
        [Route("gettreedata")]
        [HttpPost]
        public HttpResponseMessage GetTreData(HttpRequestMessage request)
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
                    var check = _Category.Get();
                    var check2 = Mapper.Map<IEnumerable<Category>, IEnumerable<TreeDataModel>>(check).ToList();
                    var responseData = "1";
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        //[Route("create")]
        //[HttpPost]
        //public HttpResponseMessage Create(HttpRequestMessage request, CategoryModel CategoryVm)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var modelVm = Mapper.Map<CategoryModel, Category>(CategoryVm);
        //            modelVm.LastUpdate = DateTime.Now;
        //            _Category.Create(modelVm);
        //            _Category.Save();

        //            var responseData = Mapper.Map<Category, CategoryModel>(modelVm);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}
        //[Route("checkcodeExistEdit")]
        //[HttpGet]
        //public HttpResponseMessage checkcodeExistEdit(HttpRequestMessage request, string CategoryCode, int? Id)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var responseData = _Category.checkCodeExist(CategoryCode, Id);
        //        response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}
        //[Route("checkcodeExist")]
        //[HttpGet]
        //public HttpResponseMessage checkcodeExist(HttpRequestMessage request, string CategoryCode)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var responseData = _Category.checkCodeExist(CategoryCode, null);
        //        response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}
        //[Route("detail/{id}")]
        //[HttpGet]
        ////[Authorize(Roles = "ViewUser")]
        //public HttpResponseMessage Details(HttpRequestMessage request, int id)
        //{
        //    HttpResponseMessage response = null;
        //    var CategoryVm = _Category.GetByID(id);
        //    var responseData = Mapper.Map<Category, CategoryModel>(CategoryVm);
        //    response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //    return response;


        //}
        ////[Route("getFile/{id}")]
        ////[HttpGet]
        ////public HttpResponseMessage GetFile(int id)
        ////{
        ////    //string fileName;
        ////    var CategoryVm = _Category.GetByID(id);


        ////        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

        ////        result.Content = new ByteArrayContent(CategoryVm.FileUpload); ;
        ////        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        ////        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        ////        result.Content.Headers.ContentDisposition.FileName = CategoryVm.FileUploadName;
        ////        return result;

        ////   // return new HttpResponseMessage(HttpStatusCode.NotFound);
        ////}
        //[Route("update")]
        //[HttpPost]
        //public HttpResponseMessage Update(HttpRequestMessage request, CategoryModel CategoryVm)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var modelVm = Mapper.Map<CategoryModel, Category>(CategoryVm);
        //            modelVm.LastUpdate = DateTime.Now;
        //            _Category.Update(modelVm);
        //            _Category.Save();

        //            var responseData = Mapper.Map<Category, CategoryModel>(modelVm);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}
        //[HttpDelete]
        //[Route("delete")]
        //public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        //{
        //    _Category.Delete(id);
        //    _Category.Save();
        //    return request.CreateResponse(HttpStatusCode.OK, id);
        //}
        //[Route("deletemulti")]
        //[HttpDelete]
        //public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
        //            foreach (var item in listItem)
        //            {
        //                _Category.Delete(item);
        //            }

        //            _Category.Save();

        //            response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
        //        }

        //        return response;
        //    });
        //}
        //[Route("getlist")]
        //[HttpGet]
        //public HttpResponseMessage GetList(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        var CategoryVm = _Category.Get(x => x.CategoryStatus == 1).OrderBy(x => x.CategoryName).Select(c => new SelectListModel { id = c.CategoryID.ToString(), name = c.CategoryName + " - " + c.CategoryFlag }).ToArray();

        //        response = request.CreateResponse(HttpStatusCode.OK, CategoryVm);
        //        return response;
        //    });
        //}
        //[Route("GetSelectedCategory")]
        //[HttpGet]
        //public HttpResponseMessage GetSelectedCategory(HttpRequestMessage request, int userID)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //       var listSelectedCategory = _UserCategory.GetUserCategoryByUserID(userID).Select(x=> x.CategoryID).ToList();
        //       var CategoryVm = _Category.Get(x => x.CategoryStatus == 1 && listSelectedCategory.Contains(x.CategoryID)).OrderBy(x => x.CategoryName).Select(c => new SelectListModel { id = c.CategoryID.ToString(), name = c.CategoryName + " - " + c.CategoryCronyms }).ToArray();

        //        response = request.CreateResponse(HttpStatusCode.OK, CategoryVm);
        //        return response;
        //    });
        //}
        //[Route("GetUnSelectedCategory")]
        //[HttpGet]
        //public HttpResponseMessage GetUnSelectedCategory(HttpRequestMessage request, int userID)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var listSelectedCategory = _UserCategory.GetUserCategoryByUserID(userID).Select(x => x.CategoryID).ToList();
        //        var CategoryVm = _Category.Get(x => x.CategoryStatus == 1 && !listSelectedCategory.Contains(x.CategoryID)).OrderBy(x => x.CategoryName).Select(c => new SelectListModel { id = c.CategoryID.ToString(), name = c.CategoryName + " - " + c.CategoryCronyms }).ToArray();

        //        response = request.CreateResponse(HttpStatusCode.OK, CategoryVm);
        //        return response;
        //    });
        //}
        //[Route("CreateUserCategory")]
        //[HttpPost]
        //public HttpResponseMessage CreateUserCategory(HttpRequestMessage request, int userID, List<SelectListModel> selectedselectedItems)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var ListCategoryID = _UserCategory.GetUserCategoryByUserID(userID);
        //        var ListDeleteUserCategory = ListCategoryID.Where(x => !selectedselectedItems.Select(c=> Convert.ToInt32(c.id)).Contains(x.CategoryID)).ToList();
        //        var ListCreateUserCategoryID = selectedselectedItems.Where(x => !ListCategoryID.Select(c=> c.CategoryID.ToString()).Contains(x.id)).ToList();
        //        _UserCategory.DelelteUserCategory(ListDeleteUserCategory);
        //        var ListCreateUserCategory = ListCreateUserCategoryID.Select(c => new UserCategory { UserID = userID, CategoryID = Convert.ToInt32(c.id) }).ToList();
        //        _UserCategory.CreateUserCategory(ListCreateUserCategory);
        //        _UserCategory.Save();
        //        response = request.CreateResponse(HttpStatusCode.Created, 0);
        //        return response;
        //    });
        //}
    }
}
