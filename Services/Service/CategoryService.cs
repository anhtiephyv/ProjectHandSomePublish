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
    public interface ICategoryService
    {
        IEnumerable<Category> Get(
          Expression<Func<Category, bool>> filter = null,
          string orderBy = null,
          string sortDir = null,
          string includeProperties = "");
        Category GetByID(int id);
        void Create(Category entity);
        void Delete(int id);
        void Delete(Category entityToDelete);
        void Update(Category entityToUpdate);
        IEnumerable<Category> GetPaging(int page = 0, int pageSize = 20,
           Expression<Func<Category, bool>> filter = null,
           string orderBy = null,
          string sortDir = null,
           string includeProperties = "");
        IEnumerable<Category> GetMultiPaging(Expression<Func<Category, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null);
        void Save();
        Category getDetail(int id);
        IEnumerable<Category> getCategoryTree(int? ParentCategory);
        //IEnumerable<Category> getCategorybyUserName(string userName);
        //bool checkCodeExist(string Code, int? ID);
    }
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _CategoryRepository;
        private IUnitOfWork _unitofWork;

        public CategoryService(ICategoryRepository CategoryRepository, IUnitOfWork unitofWork)
        {
            this._CategoryRepository = CategoryRepository;
            this._unitofWork = unitofWork;
        }

        public IEnumerable<Category> Get(Expression<Func<Category, bool>> filter = null, string orderBy = null, string sortDir = null, string includeProperties = "")
        {
            return _CategoryRepository.Get(filter, orderBy, sortDir, includeProperties);
        }

        public Category GetByID(int id)
        {
            return _CategoryRepository.GetByID(id);
        }

        public void Create(Category entity)
        {
            _CategoryRepository.Create(entity);
        }

        public void Delete(int id)
        {
            _CategoryRepository.Delete(id);
        }

        public void Delete(Category entityToDelete)
        {
            _CategoryRepository.Delete(entityToDelete);
        }

        public void Update(Category entityToUpdate)
        {
            _CategoryRepository.Update(entityToUpdate);
        }

        public IEnumerable<Category> GetPaging(int page = 0, int pageSize = 20,
           Expression<Func<Category, bool>> filter = null,
          string orderBy = null,
          string sortDir = null,
           string includeProperties = "")
        {
            IEnumerable<Category> result = _CategoryRepository.GetPaging(page, pageSize, filter, orderBy, sortDir, includeProperties);
            //     totalRow = result.ToList().Count;
            return result;
        }
        public IEnumerable<Category> GetMultiPaging(Expression<Func<Category, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null)
        {
            IEnumerable<Category> result = _CategoryRepository.GetMultiPaging(predicate, out total, orderBy, sortDir, index, size, includes);
            return result;
        }
        public void Save()
        {
            _CategoryRepository.Save();
        }
        //public bool checkCodeExist(string Code, int? ID)
        //{
        //    return _CategoryRepository.checkCodeExist(Code,ID);
        //}
        public IEnumerable<Category> getCategoryTree(int? ParentCategory)
        {
            return _CategoryRepository.getCategoryTree(ParentCategory);
        }
        public Category getDetail(int id)
        {
            return _CategoryRepository.getDetail(id);
        }

    }
}
