using SimpleBlog.DataAccess.Data;
using SimpleBlog.DataAccess.Repository.IRepository;
using SimpleBlog.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.DataAccess.Repository
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogPostRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BlogPost obj)
        {
            var objFromDb = _db.BlogPosts.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Content = obj.Content;
            }
        }
    }
}
