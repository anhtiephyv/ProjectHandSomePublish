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
        private ITagService _Tag;
        private IProductTagRepository _ProductTagReporistory;
        public ProductController(IProductService ProductService, ITagService TagService, IProductTagRepository ProductTagReporistory)
        {
            _Tag = TagService;
            _Product = ProductService;
            _ProductTagReporistory = ProductTagReporistory;
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

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductModel ProductVm)
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
                      // Create tag mới
                    var TagsCreate = ProductVm.Tags.Where(x => x.isNew == true).ToList();
                    var ListTags = Mapper.Map<List<TagModel>, List<Tag>>(TagsCreate);
                    var ListTagCreate = _Tag.AddTags(ListTags);
                  /// Tạo product
                     var modelVm = Mapper.Map<ProductModel, Product>(ProductVm);
                    _Product.Create(modelVm);
                    _Product.Save();
                    /// Tag tag
                    List<ProductTag> ListProductTag = new List<ProductTag>();
                    foreach(var Tag in ProductVm.Tags.Where(x => x.isNew == false).ToList())
                    {
                        ProductTag ProductTag = new ProductTag();
                        ProductTag.ProductID = modelVm.ProductID;
                        ProductTag.TagID = Convert.ToInt32(Tag.id);
                        ListProductTag.Add(ProductTag);
                    }
                    foreach (var Tag in ListTagCreate)
                    {
                        ProductTag ProductTag = new ProductTag();
                        ProductTag.ProductID = modelVm.ProductID;
                        ProductTag.TagID = Tag.TagID;
                        ListProductTag.Add(ProductTag);
                    }
                    _ProductTagReporistory.CreateProductTag(ListProductTag);
                    _ProductTagReporistory.Save();

                    var responseData = Mapper.Map<Product, ProductModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
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
        [Route("detail/{id}")]
        [HttpGet]
        //[Authorize(Roles = "ViewUser")]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            var ProductVm = _Product.GetByID(id);
            var responseData = Mapper.Map<Product, ProductModel>(ProductVm);
            response = request.CreateResponse(HttpStatusCode.OK, responseData);
            return response;


        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductModel ProductVm)
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
                    // Create tag mới
                    var TagsCreate = ProductVm.Tags.Where(x => x.isNew == true).ToList();
                    var ListTags = Mapper.Map<List<TagModel>, List<Tag>>(TagsCreate);
                    var ListTagCreate = _Tag.AddTags(ListTags);
                    /// Update product
                    var modelVm = Mapper.Map<ProductModel, Product>(ProductVm);
                    _Product.Update(modelVm);
                    _Product.Save();
                    // select các tag cũ
                    var AllOldTag = _ProductTagReporistory.GetProductTagByProductID(modelVm.ProductID);
               //     var AllDeletedTag = AllOldTag.Intersect(ProductVm.Tags.Where());
                    /// Tag tag
                    List<ProductTag> ListProductTag = new List<ProductTag>();
                    foreach (var Tag in ProductVm.Tags.Where(x => x.isNew == false).ToList())
                    {
                        ProductTag ProductTag = new ProductTag();
                        ProductTag.ProductID = modelVm.ProductID;
                        ProductTag.TagID = Convert.ToInt32(Tag.id);
                        ListProductTag.Add(ProductTag);
                    }
                    foreach (var Tag in ListTagCreate)
                    {
                        ProductTag ProductTag = new ProductTag();
                        ProductTag.ProductID = modelVm.ProductID;
                        ProductTag.TagID = Tag.TagID;
                        ListProductTag.Add(ProductTag);
                    }
                    _ProductTagReporistory.CreateProductTag(ListProductTag);
                    _ProductTagReporistory.Save();

                    var responseData = Mapper.Map<Product, ProductModel>(modelVm);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }

                return response;
            });
        }
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
        [Route("createtag")]
        [HttpPost]
        public HttpResponseMessage CreateTag(HttpRequestMessage request, List<TagModel> TagsVm)
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

                    var TagsCreate = TagsVm.Where(x => x.isNew == true).ToList();
                    var ListTags = Mapper.Map<List<TagModel>, List<Tag>>(TagsCreate);
                    var ListTagCreate = _Tag.AddTags(ListTags);
                    var responseData = true;
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [Route("getlisttag")]
        [HttpGet]
        public HttpResponseMessage getlisttag(HttpRequestMessage request)
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
                    var AllTag = _Tag.Get().ToList();
                    var responseData = Mapper.Map<List<Tag>, List<TagModel>>(AllTag);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }
        [Route("getlisttagbyproductid/{id}")]
        [HttpGet]
        public HttpResponseMessage getlisttagbyProductId(HttpRequestMessage request,int id)
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
                    var AllTag = _ProductTagReporistory.GetProductTagByProductID(id);
                    var responseData = Mapper.Map<List<Tag>, List<TagModel>>(AllTag);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }
    }
}
