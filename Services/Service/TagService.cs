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
    public interface ITagService
    {
        IEnumerable<Tag> Get(
          Expression<Func<Tag, bool>> filter = null,
          string orderBy = null,
          string sortDir = null,
          string includeProperties = "");
        Tag GetByID(int id);
        void Create(Tag entity);
        void Delete(int id);
        void Delete(Tag entityToDelete);
        void Update(Tag entityToUpdate);
        IEnumerable<Tag> GetPaging( int page = 0, int pageSize = 20,
           Expression<Func<Tag, bool>> filter = null,
           string orderBy = null,
          string sortDir = null,
           string includeProperties = "");
        IEnumerable<Tag> GetMultiPaging(Expression<Func<Tag, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null);
        void Save();
        List<Tag> AddTags(List<Tag> Tags);
    }
    public class TagService : ITagService
    {
        private ITagRepository _TagRepository;
        private IUnitOfWork _unitofWork;

        public TagService(ITagRepository TagRepository, IUnitOfWork unitofWork)
        {
            this._TagRepository = TagRepository;
            this._unitofWork = unitofWork;
        }

        public IEnumerable<Tag> Get(Expression<Func<Tag, bool>> filter = null, string orderBy = null, string sortDir = null, string includeProperties = "")
        {
            return _TagRepository.Get(filter, orderBy, sortDir, includeProperties);
        }

        public Tag GetByID(int id)
        {
            return _TagRepository.GetByID(id);
        }

        public void Create(Tag entity)
        {
            _TagRepository.Create(entity);
        }

        public void Delete(int id)
        {
            _TagRepository.Delete(id);
        }

        public void Delete(Tag entityToDelete)
        {
            _TagRepository.Delete(entityToDelete);
        }

        public void Update(Tag entityToUpdate)
        {
            _TagRepository.Update(entityToUpdate);
        }

        public IEnumerable<Tag> GetPaging( int page = 0, int pageSize = 20,
           Expression<Func<Tag, bool>> filter = null,
          string orderBy = null,
          string sortDir = null,
           string includeProperties = "")
        {
            IEnumerable<Tag> result = _TagRepository.GetPaging(page, pageSize, filter, orderBy, sortDir, includeProperties);
       //     totalRow = result.ToList().Count;
            return result;
        }
        public IEnumerable<Tag> GetMultiPaging(Expression<Func<Tag, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null)
        {
            IEnumerable<Tag> result = _TagRepository.GetMultiPaging(predicate, out total, orderBy, sortDir, index, size, includes);
            return result;
        }
        public void Save()
        {
            _TagRepository.Save();
        }
        public List<Tag> AddTags(List<Tag> Tags)
        {
            _TagRepository.AddTags(Tags);
             _TagRepository.Save();
             return Tags;
        }

    }
}
