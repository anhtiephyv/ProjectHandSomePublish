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
    [RoutePrefix("api/Product")]
    [Authorize]
    public class ProductController : BaseController
    {
        private IProductService _Product;
        public ProductController(IProductService ProductService)
        {
            _Product = ProductService;
        }
        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyword, int page, int pageSize, string orderby, string sortDir, string filter = null)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _Product.GetMultiPaging(x => x.ProductName.Contains(keyword) || string.IsNullOrEmpty(keyword), out totalRow, orderby, sortDir, page, pageSize, new string[] { "ProductCategory" });
                var check = model.ToList();
                IEnumerable<ProductModel> modelVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(model);

                PaginationSet<ProductModel> pagedSet = new PaginationSet<ProductModel>()
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

        //[Route("create")]
        //[HttpPost]
        //public HttpResponseMessage Create(HttpRequestMessage request, ProductModel ProductVm)
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
        //            var modelVm = Mapper.Map<ProductModel, Product>(ProductVm);

        //            _Product.Create(modelVm);
        //            _Product.Save();

        //            var responseData = Mapper.Map<Product, ProductModel>(modelVm);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}
        //[Route("checkcodeExistEdit")]
        //[HttpGet]
        //public HttpResponseMessage checkcodeExistEdit(HttpRequestMessage request, string ProductCode, int? Id)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var responseData = _Product.checkCodeExist(ProductCode, Id);
        //        response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}
        //[Route("checkcodeExist")]
        //[HttpGet]
        //public HttpResponseMessage checkcodeExist(HttpRequestMessage request, string ProductCode)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var responseData = _Product.checkCodeExist(ProductCode, null);
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
        //    var ProductVm = _Product.GetByID(id);
        //    var responseData = Mapper.Map<Product, ProductModel>(ProductVm);
        //    response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //    return response;


        //}

        //[Route("update")]
        //[HttpPost]
        //public HttpResponseMessage Update(HttpRequestMessage request, ProductModel ProductVm)
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
        //            var modelVm = Mapper.Map<ProductModel, Product>(ProductVm);
        //            _Product.Update(modelVm);
        //            _Product.Save();

        //            var responseData = Mapper.Map<Product, ProductModel>(modelVm);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            _Product.Delete(id);
            _Product.Save();
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
                        _Product.Delete(item);
                    }

                    _Product.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}
