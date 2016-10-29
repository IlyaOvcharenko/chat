using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace DataAccess.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User FindUserByLogin(string login);
    }
}
