using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {  
        }

        public User FindUserByLogin(string login)
        {
            return DataContext.Set<User>().FirstOrDefault(u => u.Login == login);
        }
    }
}
