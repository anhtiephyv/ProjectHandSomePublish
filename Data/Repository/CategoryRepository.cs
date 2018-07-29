using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DBContext;
using Data.Models;
using Data.Base;
using System.Data.Entity;
namespace Data.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //bool checkCodeExist(string Code, int? ID);
        IEnumerable<Category> getCategoryTree(int? ParentCategory);
       Category getDetail(int id);
    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyShopDBContext DBcontext)
            : base(DBcontext)
        {
        }
        public IEnumerable<Category> getCategoryTree(int? ParentCategory)
        {
            return DbContext.Category.Include(x => x.CategoryChildren).Where(c => c.ParentCategory == ParentCategory.Value);
        }
        public Category getDetail(int id)
        {
            var check = DbContext.Category.Find(id);
            return check;
        }
    }
}
