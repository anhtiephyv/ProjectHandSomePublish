using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Data.DBContext;
using Data.Models;
namespace Data.Base
{
    public class UnitOfWork: IUnitOfWork
    {
        private MyShopDBContext context = new MyShopDBContext();
      //  private GenericRepository<Admin> adminRepository;

        public MyShopDBContext Init(){
            return context ?? (context = new MyShopDBContext());
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}