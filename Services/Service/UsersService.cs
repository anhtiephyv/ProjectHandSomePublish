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
    public interface IUsersService
    {
        IEnumerable<Users> Get(
          Expression<Func<Users, bool>> filter = null,
          string orderBy = null,
          string sortDir = null,
          string includeProperties = "");
        Users GetByID(int id);
        void Create(Users entity);
        void Delete(int id);
        void Delete(Users entityToDelete);
        void Update(Users entityToUpdate);
        IEnumerable<Users> GetMultiPaging(Expression<Func<Users, bool>> predicate, out int total, string orderBy = null,
            string sortDir = null, int index = 0, int size = 20, string[] includes = null);
        void Save();
        bool checkCodeExist(string Code, int? ID);
    }
    public class UsersService : IUsersService
    {
        private IUsersRepository _usersRepository;
        private IUnitOfWork _unitofWork;

        public UsersService(IUsersRepository UsersRepository, IUnitOfWork unitofWork)
        {
            this._usersRepository = UsersRepository;
            this._unitofWork = unitofWork;
        }

        public IEnumerable<Users> Get(Expression<Func<Users, bool>> filter = null, string orderBy = null, string sortDir = null, string includeProperties = "")
        {
            return _usersRepository.Get(filter, orderBy, sortDir, includeProperties);
        }

        public Users GetByID(int id)
        {
            return _usersRepository.GetByID(id);
        }

        public void Create(Users entity)
        {
            _usersRepository.Create(entity);
        }

        public void Delete(int id)
        {
            _usersRepository.Delete(id);
        }

        public void Delete(Users entityToDelete)
        {
            _usersRepository.Delete(entityToDelete);
        }

        public void Update(Users entityToUpdate)
        {
            _usersRepository.Update(entityToUpdate);
        }


        public IEnumerable<Users> GetMultiPaging(Expression<Func<Users, bool>> predicate, out int total, string orderBy = null,
    string sortDir = null, int index = 0, int size = 20, string[] includes = null)
        {
            IEnumerable<Users> result = _usersRepository.GetMultiPaging(predicate, out total, orderBy, sortDir, index, size, includes);
            return result;
        }
        public void Save()
        {
            _usersRepository.Save();
        }
        public bool checkCodeExist(string Code, int? ID)
        {
            return _usersRepository.checkCodeExist(Code, ID);
        }
    }
}
