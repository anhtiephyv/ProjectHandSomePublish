using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
namespace Data.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // Get by expression
        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           string orderBy = null, string sortDir = null,
           string includeProperties = "");
        //Get by id
        TEntity GetByID(object id);
        //Create
        void Create(TEntity entity);
        //Delete
        void Delete(object id);
        // Delete by entity
        void Delete(TEntity entityToDelete);
        //Update
        void Update(TEntity entityToUpdate);
        IEnumerable<TEntity> GetPaging( int page = 0, int pageSize = 20,
         Expression<Func<TEntity, bool>> filter = null,
        string orderBy = null,string sortDir = null,
         string includeProperties = "");
        IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null);
        void Save();
    }
}
