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
namespace MyProject.Api
{
    [RoutePrefix("api/Download")]
    [AllowAnonymous]
    public class DownloadController : BaseController
    {
        //private IAppUsersService _usersService;
        private ICountryService _Country;
        // private ICountryRepository _CountryRepository;
        public DownloadController(ICountryService CountryService)
        {
            _Country = CountryService;
            //   _CountryRepository = CountryRepository;
        }
        [Route("getFile/{id}")]
        [HttpGet]
        public HttpResponseMessage GetFile(int id)
        {
            //string fileName;
            var countryVm = _Country.GetByID(id);


                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                result.Content = new ByteArrayContent(countryVm.FileUpload); ;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = countryVm.FileUploadName;
                return result;
            
           // return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
