using AutoMapper;
using Data.Models;
using MyProject.Model;

namespace MyProject.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {

            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<Post, PostViewModel>();
                //cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                //cfg.CreateMap<Tag, TagViewModel>();
                //cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                //cfg.CreateMap<Product, ProductViewModel>();
                //cfg.CreateMap<ProductTag, ProductTagViewModel>();
                //cfg.CreateMap<Footer, FooterViewModel>();
                //cfg.CreateMap<Slide, SlideViewModel>();
                //cfg.CreateMap<Page, PageViewModel>();
                //cfg.CreateMap<ContactDetail, ContactDetailViewModel>();
                //cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                cfg.CreateMap<Country, CountryModel>();
                cfg.CreateMap<ApplicationUser, AdminModel>();
                cfg.CreateMap<Users, UsersModel>();
            });
        }

    }
}