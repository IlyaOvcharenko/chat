using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Paging;
using Data;
using DataAccess;
using DataAccess.Repositories;
using Utility;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private ICryptoManager _cryptoManager;

        public UserService(IUserRepository userRepository, ICryptoManager cryptoManager)
        {
            _userRepository = userRepository;
            _cryptoManager = cryptoManager;
        }

        public EntityDataPage<User> GetUsersPage(int pageNumber, int pageSize)
        {
            var list =
                _userRepository.GetAll()
                    .OrderBy(t => t.Login)
                    .Skip(pageSize * pageNumber)
                    .Take(pageSize)
                    .ToList();
            return new EntityDataPage<User>
            {
                EntityCount = list.Count,
                List = list,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public void Register(string login, string password, string city)
        {
            var user = new User { Login = login, Password = _cryptoManager.GetHash(password), City = city};
            _userRepository.Create(user);
        }

        public bool ValidateUser(string login, string password)
        {
            var user = _userRepository.FindUserByLogin(login);
            return user != null && _cryptoManager.VarifyHash(user.Password, password);
        }


        public bool IsLoginExist(string login)
        {
            return _userRepository.FindUserByLogin(login) != null;
        }

        public User GetUserByLogin(string login)
        {
            return _userRepository.FindUserByLogin(login);
        }
    }
}
