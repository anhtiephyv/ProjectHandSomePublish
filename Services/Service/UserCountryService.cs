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
    public interface IUserCountryService
    {
        void CreateUserCountry(List<UserCountry> _listUserCountry);
        void DelelteUserCountry(List<UserCountry> _listUserCountry);
        List<UserCountry> GetUserCountryByUserID(int userID);
        void Save();

    }
    public class UserCountryService : IUserCountryService
    {
        private IUserCountryRepository _userCountryRepository;
        private IUnitOfWork _unitofWork;

        public UserCountryService(IUserCountryRepository UserCountryRepository, IUnitOfWork unitofWork)
        {
            this._userCountryRepository = UserCountryRepository;
            this._unitofWork = unitofWork;
        }
        public void CreateUserCountry(List<UserCountry> _listUserCountry)
        {
            _userCountryRepository.CreateUserCountry(_listUserCountry);
        }
        public void DelelteUserCountry(List<UserCountry> _listUserCountry)
        {
            _userCountryRepository.DeleteUserCountry(_listUserCountry);
        }
        public List<UserCountry> GetUserCountryByUserID(int userID)
        {
            return _userCountryRepository.GetUserCountryByUserID(userID);
        }
        public void Save()
        {
            _userCountryRepository.Save();
        }
    }
}
