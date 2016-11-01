using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Paging;
using Data;

namespace BusinessLogic
{
    public interface IUserService
    {

        void Register(string login, string password, string city);

        bool ValidateUser(string login, string password);

        bool IsLoginExist(string login);

        User GetUserByLogin(string login);
    }
}
