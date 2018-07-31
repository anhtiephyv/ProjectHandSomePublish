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
    public interface IProductRepository : IGenericRepository<Product>
    {
       // //bool checkCodeExist(string Code, int? ID);
       // IEnumerable<Product> getProductTree(int? ParentProduct);
       //Product getDetail(int id);
    }
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MyShopDBContext DBcontext)
            : base(DBcontext)
        {
        }
        //public IEnumerable<Product> getProductTree(int? ParentProduct)
        //{
        //    return DbContext.Product.Include(x => x.ProductChildren).Where(c => c.ParentProductID == ParentProduct.Value);
        //}
        //public Product getDetail(int id)
        //{
        //    var check = DbContext.Product.Find(id);
        //    return check;
        //}
    }
}
