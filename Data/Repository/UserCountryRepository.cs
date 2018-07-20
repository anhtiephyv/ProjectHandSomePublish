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
    public interface IUserCountryRepository : IGenericRepository<UserCountry>
    {
        void DeleteUserCountry(List<UserCountry> _listUserCountry);
        void CreateUserCountry(List<UserCountry> _listUserCountry);
        List<UserCountry> GetUserCountryByUserID(int userID);
    }
    public class UserCountryRepository : GenericRepository<UserCountry>, IUserCountryRepository
    {
        public UserCountryRepository(MyShopDBContext DBcontext)
            : base(DBcontext)
        {

        }
        public void CreateUserCountry(List<UserCountry> _listUserCountry)
        {
            DbContext.UserCountry.AddRange(_listUserCountry);
        }
        public List<UserCountry> GetUserCountryByUserID(int userID)
        {
           return DbContext.UserCountry.Where(x => x.UserID == userID).ToList();
        }
        public void DeleteUserCountry(List<UserCountry> _listUserCountry)
        {
            DbContext.UserCountry.RemoveRange(_listUserCountry);
        }
    }
}
