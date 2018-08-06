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
    public interface ITagRepository : IGenericRepository<Tag>
    {
        void AddTags(List<Tag> Tags);
    }
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(MyShopDBContext DBcontext)
            : base(DBcontext)
        {

        }
        public void AddTags(List<Tag> Tags)
        {
            DbContext.Tag.AddRange(Tags);
        }
    }
}
