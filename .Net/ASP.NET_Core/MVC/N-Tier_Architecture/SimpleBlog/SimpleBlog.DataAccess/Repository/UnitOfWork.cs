using SimpleBlog.DataAccess.Data;
using SimpleBlog.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SimpleBlog.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IBlogPostRepository BlogPost { get; private set; }

        public ICommentRepository Comment { get; private set; }

        public ISubscriberRepository Subscriber { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
           
            BlogPost = new BlogPostRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Comment = new CommentRepository(_db);
            Subscriber = new SubscriberRepository(_db);

        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
