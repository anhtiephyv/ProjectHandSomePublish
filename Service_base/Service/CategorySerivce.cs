using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Data.Base;
using System.Threading.Tasks;

namespace Service.Service
{
    public interface ICategorySerivce
    {

        IEnumerable<Category> GetAll();
    }
    public class CategorySerivce : ICategorySerivce
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public CategorySerivce()
        {
        }
        public IEnumerable<Category> GetAll()
        {

            var check = unitOfWork.CategoryRepository.Get().ToList();
            return check;
        }
    }
}
