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
                var model = _Category.GetMultiPaging(x => x.CategoryName.Contains(keyword) || string.IsNullOrEmpty(keyword), out totalRow, orderby, sortDir, page, pageSize, null);
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
        [HttpGet]
        public HttpResponseMessage GetTreeData(HttpRequestMessage request,int? ParentCategory = null)
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
                    var TreeDataVM = _Category.getCategoryTree(ParentCategory);
                    var TreeDataVMlist = TreeDataVM.ToList();
                    var TreeData = Mapper.Map<IEnumerable<Category>, IEnumerable<TreeDataModel>>(TreeDataVM).ToList();
                    var responseData = TreeData;
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CategoryModel CategoryVm)
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
                    var modelVm = Mapper.Map<CategoryModel, Category>(CategoryVm);
                   
                    _Category.Create(modelVm);
                    _Category.Save();

                    var responseData = Mapper.Map<Category, CategoryModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
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
        [Route("detail/{id}")]
        [HttpGet]
        //[Authorize(Roles = "ViewUser")]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            var CategoryVm = _Category.GetByID(id);
            var check = _Category.getDetail(id);
            var fucl = _Category.Get();
            var responseData = Mapper.Map<Category, CategoryModel>(CategoryVm);
            response = request.CreateResponse(HttpStatusCode.OK, responseData);
            return response;


        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, CategoryModel CategoryVm)
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
                    var modelVm = Mapper.Map<CategoryModel, Category>(CategoryVm);
                    _Category.Update(modelVm);
                    _Category.Save();

                    var responseData = Mapper.Map<Category, CategoryModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            _Category.Delete(id);
            _Category.Save();
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
                        _Category.Delete(item);
                    }

                    _Category.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}
