using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace DataAccess.Repositories
{
    public class UserActivityRepository : BaseRepository<UserActivity>
    {
        public UserActivityRepository(DataContext dataContext) : base(dataContext)
        {
            DataContext.Configuration.AutoDetectChangesEnabled = false;
        }

        public override void SaveChanges()
        {
            DataContext.ChangeTracker.DetectChanges();
            base.SaveChanges();
        }
    }
}
