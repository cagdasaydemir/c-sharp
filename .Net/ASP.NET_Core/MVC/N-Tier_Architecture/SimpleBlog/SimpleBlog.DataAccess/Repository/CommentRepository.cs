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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
