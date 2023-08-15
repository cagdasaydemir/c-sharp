using SimpleBlog.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.DataAccess.Repository.IRepository
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        void Update(BlogPost obj);
    }
}
