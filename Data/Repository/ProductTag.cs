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
    public interface IProductTagRepository : IGenericRepository<ProductTag>
    {
        void DeleteProductTag(List<ProductTag> _listProductTag);
        void CreateProductTag(List<ProductTag> _listProductTag);
        List<ProductTag> GetProductTagByProductID(int productID);
    }
    public class ProductTagRepository : GenericRepository<ProductTag>, IProductTagRepository
    {
        public ProductTagRepository(MyShopDBContext DBcontext)
            : base(DBcontext)
        {

        }
        public void CreateProductTag(List<ProductTag> _listProductTag)
        {
            DbContext.ProductTag.AddRange(_listProductTag);
        }
        public List<ProductTag> GetProductTagByProductID(int productID)
        {
            return DbContext.ProductTag.Where(x => x.ProductID == productID).ToList();
        }
        public void DeleteProductTag(List<ProductTag> _listProductTag)
        {
            DbContext.ProductTag.RemoveRange(_listProductTag);
        }
    }
}
