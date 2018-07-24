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
              .ForMember(des => des.CategoryLevel, mo => mo.MapFrom(src => src.CategoryParent.CategoryLevel))
              .ForMember(des => des.DisplayOrder, mo => mo.MapFrom(src => src.DisplayOrder))
              .ForAllOtherMembers(opts => opts.Ignore());
                cfg.CreateMap<Category, TreeDataModel>()
.ForMember(des => des.id, mo => mo.MapFrom(src => src.CategoryID))
.ForMember(des => des.label, mo => mo.MapFrom(src => src.CategoryName))
.ForMember(des => des.collapsed, mo => mo.MapFrom(src => src.CategoryChildren.Any()))
.ForMember(des => des.children, mo => mo.MapFrom(src => src.CategoryChildren.ToArray()))
.ForAllOtherMembers(opts => opts.Ignore());
            });

        }


    }
}