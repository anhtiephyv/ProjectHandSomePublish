using AutoMapper;
using Data.Models;
using MyProject.Model;
using MyProject.helper;
using System.Linq;
namespace MyProject.Mapping
{
    public class AutoMapperConfiguration
    {


        public static void Configure()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Country, CountryModel>();
                cfg.CreateMap<ApplicationUser, AdminModel>();
                cfg.CreateMap<Users, UsersModel>();
                // Map chi tiết của Category

                //     cfg.CreateMap<Category, CategoryModel>().IgnoreAll();
                cfg.CreateMap<Category, CategoryModel>()
              .ForMember(des => des.CategoryID, mo => mo.MapFrom(src => src.CategoryID))
              .ForMember(des => des.CategoryName, mo => mo.MapFrom(src => src.CategoryName))
              .ForMember(des => des.CategoryLevel, mo => mo.MapFrom(src => src.CategoryLevel))
              .ForMember(des => des.Description, mo => mo.MapFrom(src => src.Description))
              .ForMember(des => des.ParentName, mo => mo.MapFrom(src => src.CategoryParent.CategoryName))
              .ForMember(des => des.DisplayOrder, mo => mo.MapFrom(src => src.DisplayOrder))
              .ForAllOtherMembers(opts => opts.Ignore());
                cfg.CreateMap<CategoryModel, Category>()
.ForMember(des => des.CategoryID, mo => mo.MapFrom(src => src.CategoryID))
.ForMember(des => des.CategoryName, mo => mo.MapFrom(src => src.CategoryName))
.ForMember(des => des.CategoryLevel, mo => mo.MapFrom(src => src.CategoryLevel))
.ForMember(des => des.Description, mo => mo.MapFrom(src => src.Description))
.ForMember(des => des.ParentCategoryID, mo => mo.MapFrom(src => src.ParentCategory))
.ForMember(des => des.CategoryLevel, mo => mo.MapFrom(src => src.CategoryLevel))
.ForMember(des => des.DisplayOrder, mo => mo.MapFrom(src => src.DisplayOrder));

                cfg.CreateMap<Category, TreeDataModel>()
.ForMember(des => des.id, mo => mo.MapFrom(src => src.CategoryID))
.ForMember(des => des.text, mo => mo.MapFrom(src => src.CategoryName))
.ForMember(des => des.lazyLoad, mo => mo.MapFrom(src => src.CategoryChildren.Any()))
.ForMember(des => des.tags, mo => mo.MapFrom(src => src.CategoryChildren.Count.ToString().ToArray()))
.ForAllOtherMembers(opts => opts.Ignore());
            });

        }


    }
}