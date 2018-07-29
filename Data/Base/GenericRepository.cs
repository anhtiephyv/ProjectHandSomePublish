using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Models;
using Data.DBContext;
using System.Data.Entity;
using Data.Base;
using System.Linq.Expressions;
using System.Reflection;
namespace Data.Base
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //Class variables are declared for the database context and for the entity set that the repository is instantiated for
        internal MyShopDBContext context;
        internal DbSet<TEntity> dbSet;
      
        //The constructor accepts a database context instance and initializes the entity set variable:
        public GenericRepository(MyShopDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        protected IUnitOfWork UnitOfWork
        {
            get;
            private set;
        }
        protected MyShopDBContext DbContext
        {
            get { return context ?? (context = UnitOfWork.Init()); }
        }
        //The Get method uses lambda expressions to allow the calling code to specify a filter condition and a column to order the results by, and a string parameter lets the caller provide a comma-delimited list of navigation properties for eager loading
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
            string sortDir = null,
            string includeProperties = "")
        {
            /* The code Expression<Func<TEntity, bool>> filter means the caller will provide a lambda expression based on the TEntity type, and this expression will return a Boolean value. For example, if the repository is instantiated for the Student entity type, the code in the calling method might specify student => student.LastName == "Smith" for the filter parameter.

The code Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy also means the caller will provide a lambda expression. But in this case, the input to the expression is an IQueryable object for the TEntity type. The expression will return an ordered version of that IQueryable object. For example, if the repository is instantiated for the Student entity type, the code in the calling method might specify q => q.OrderBy(s => s.LastName) for the orderBy parameter.

The code in the Get method creates an IQueryable object and then applies the filter expression if there is one*/
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            //Next it applies the eager-loading expressions after parsing the comma-delimited list
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            //Finally, it applies the orderBy expression if there is one and returns the results; otherwise it returns the results from the unordered query
            if (orderBy != null)
            {
                return query.OrderByName(orderBy, sortDir);
            }
            else
            {
                return query.ToList();
            }
        }
        // Get paging
        // Của lão thế viết như cc
        //public virtual IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
        //{
        //    int skipCount = index * size;
        //    IQueryable<TEntity> _resetSet;

        //    //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
        //    if (includes != null && includes.Count() > 0)
        //    {
        //        var query = DbContext.Set<TEntity>().Include(includes.First());
        //        foreach (var include in includes.Skip(1))
        //            query = query.Include(include);
        //        _resetSet = predicate != null ? query.Where<TEntity>(predicate).AsQueryable() : query.AsQueryable();
        //    }
        //    else
        //    {
        //        _resetSet = predicate != null ? DbContext.Set<TEntity>().Where<TEntity>(predicate).AsQueryable() : DbContext.Set<TEntity>().AsQueryable();
        //    }

        //    _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
        //    total = _resetSet.Count();
        //    return _resetSet.AsQueryable();
        //}
        // Xem của Tiệp thần côn viết đây
        public virtual IEnumerable<TEntity> GetPaging(int page = 0, int pageSize = 20,
    Expression<Func<TEntity, bool>> filter = null,
   string orderBy = null,
            string sortDir = null,
    string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                var OrderExpression = GetOrderBy(orderBy, sortDir);
              //  OrderExpression(query).sk;
            }
            int skipCount = page * pageSize;
            //   qu
              query = query.Skip(skipCount).Take(pageSize);

            return query;
        }
        // Get By ID
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
 
        }
        //Create
        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }
        //Delete
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
          
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        //Update
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IEnumerable<TEntity> GetMultiPaging(Expression<Func<TEntity, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null)
        {

            int skipCount = index * size;
            IQueryable<TEntity> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<TEntity>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dbSet.Where<TEntity>(predicate).AsQueryable() : dbSet.AsQueryable();
            }
            total = _resetSet.Count();
            _resetSet = skipCount == 0 ? GetOrderBy(orderBy, sortDir)(_resetSet).Take(size) : GetOrderBy(orderBy, sortDir)(_resetSet).Skip(skipCount).Take(size);

            return _resetSet.AsQueryable();
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderBy(string orderColumn, string orderType)
        {
            Type typeQueryable = typeof(IQueryable<TEntity>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumn.Split('.');
            IQueryable<TEntity> query = new List<TEntity>().AsQueryable<TEntity>();
            Type type = typeof(TEntity);
            ParameterExpression arg = Expression.Parameter(type, "x");

            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType == "asc" ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            return (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)finalLambda.Compile();
        }
    }
}