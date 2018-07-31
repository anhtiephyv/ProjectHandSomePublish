using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Data.Base;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Data.Repository;
namespace Service.Service
{
    public interface IProductService
    {
        IEnumerable<Product> Get(
          Expression<Func<Product, bool>> filter = null,
          string orderBy = null,
          string sortDir = null,
          string includeProperties = "");
        Product GetByID(int id);
        void Create(Product entity);
        void Delete(int id);
        void Delete(Product entityToDelete);
        void Update(Product entityToUpdate);
        IEnumerable<Product> GetPaging(int page = 0, int pageSize = 20,
           Expression<Func<Product, bool>> filter = null,
           string orderBy = null,
          string sortDir = null,
           string includeProperties = "");
        IEnumerable<Product> GetMultiPaging(Expression<Func<Product, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null);
        void Save();
        //Product getDetail(int id);
        //IEnumerable<Product> getProductTree(int? ParentProduct);
        //IEnumerable<Product> getProductbyUserName(string userName);
        //bool checkCodeExist(string Code, int? ID);
    }
    public class ProductService : IProductService
    {
        private IProductRepository _ProductRepository;
        private IUnitOfWork _unitofWork;

        public ProductService(IProductRepository ProductRepository, IUnitOfWork unitofWork)
        {
            this._ProductRepository = ProductRepository;
            this._unitofWork = unitofWork;
        }

        public IEnumerable<Product> Get(Expression<Func<Product, bool>> filter = null, string orderBy = null, string sortDir = null, string includeProperties = "")
        {
            return _ProductRepository.Get(filter, orderBy, sortDir, includeProperties);
        }

        public Product GetByID(int id)
        {
            return _ProductRepository.GetByID(id);
        }

        public void Create(Product entity)
        {
            _ProductRepository.Create(entity);
        }

        public void Delete(int id)
        {
            _ProductRepository.Delete(id);
        }

        public void Delete(Product entityToDelete)
        {
            _ProductRepository.Delete(entityToDelete);
        }

        public void Update(Product entityToUpdate)
        {
            _ProductRepository.Update(entityToUpdate);
        }

        public IEnumerable<Product> GetPaging(int page = 0, int pageSize = 20,
           Expression<Func<Product, bool>> filter = null,
          string orderBy = null,
          string sortDir = null,
           string includeProperties = "")
        {
            IEnumerable<Product> result = _ProductRepository.GetPaging(page, pageSize, filter, orderBy, sortDir, includeProperties);
            //     totalRow = result.ToList().Count;
            return result;
        }
        public IEnumerable<Product> GetMultiPaging(Expression<Func<Product, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null)
        {
            IEnumerable<Product> result = _ProductRepository.GetMultiPaging(predicate, out total, orderBy, sortDir, index, size, includes);
            return result;
        }
        public void Save()
        {
            _ProductRepository.Save();
        }
        //public bool checkCodeExist(string Code, int? ID)
        //{
        //    return _ProductRepository.checkCodeExist(Code,ID);
        //}


    }
}
