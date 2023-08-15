using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IBlogPostRepository BlogPost { get; }
        ICommentRepository Comment { get; }
        ISubscriberRepository Subscriber { get; }
        void Save();
    }
}
