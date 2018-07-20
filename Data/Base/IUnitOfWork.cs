using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.DBContext;
namespace Data.Base
{
    public interface IUnitOfWork : IDisposable
    {
       // void Dispose();
        void Save();
        MyShopDBContext Init();

    }
}
