using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Data.Base;
using Data.Repository;
using System.Threading.Tasks;

namespace Service.Service
{
    public interface IAdminService    {

        IEnumerable<Admin> GetAll();
    }
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
       // private IUnitOfWork _unitOfWork;

        public AdminService(IAdminRepository AdminRepository)
        {
           // this._ProductCategoryRepository = ProductCategoryRepository;
            this._adminRepository = AdminRepository;
        }

        private UnitOfWork unitOfWork = new UnitOfWork();
        private CategorySerivce categoryservice = new CategorySerivce();
        //public AdminService()
        //{
        //}
        public IEnumerable<Admin> GetAll()
        {

            var check = unitOfWork.AdminRepository.Get().ToList();
            var check1 = _adminRepository.Get().ToList();
            var check2 = unitOfWork.CategoryRepository.Get().ToList();
            return check;
        }
    }
}
