using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DBContext;
using Data.Models;
using Data.Base;
namespace Data.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //bool checkCodeExist(string Code, int? ID);
        //IEnumerable<Category> getCategorybyUserName(string userName);
    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyShopDBContext DBcontext)
            : base(DBcontext)
        {
        }
        //public bool checkCodeExist(string Code, int? ID)
        //{
        //    if (!ID.HasValue)
        //    {
        //        return DbContext.Category.Any(x => x.CategoryCronyms == Code);
        //    }
        //    return DbContext.Category.Any(x => x.CategoryCronyms == Code && x.CategoryID != ID);
        //}
        //public IEnumerable<Category> getCategorybyUserName(string userName)
        //{

        //    var query = from c in DbContext.Category
        //                join uc in DbContext.UserCategory
        //                on c.CategoryID equals uc.CategoryID
        //                join u in DbContext.UsersUndefined
        //                 on uc.UserID equals u.UserID
        //                where c.CategoryStatus != 2 && u.UserName == userName
        //                select c;
        //    return query;
        //}
    }
}
