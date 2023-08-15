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
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        private readonly ApplicationDbContext _db;
        public SubscriberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Subscriber obj)
        {
            var objFromDb = _db.Subscribers.FirstOrDefault(x => x.Email == obj.Email);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Email = obj.Email;
                objFromDb.IsSubscribed = obj.IsSubscribed;
                objFromDb.DateSubscribed = obj.DateSubscribed;
                objFromDb.DateUnSubscribed = obj.DateUnSubscribed;

              
            }
        }

        
    }
}
