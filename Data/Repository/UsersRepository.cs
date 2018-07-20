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
    public interface IUsersRepository : IGenericRepository<Users>
    {
        bool checkCodeExist(string Code, int? ID);
    }
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository(MyShopDBContext DBcontext)
            : base(DBcontext)
        {
        }
        public bool checkCodeExist(string Code, int? ID)
        {
            if (!ID.HasValue)
            {
                return DbContext.UsersUndefined.Any(x => x.UserName == Code);
            }
            return DbContext.UsersUndefined.Any(x => x.UserName == Code && x.UserID != ID);
        }
    }
}
