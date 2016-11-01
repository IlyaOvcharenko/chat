using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Paging;
using Data;
using Data.Enums;
using DataAccess;
using DataAccess.Repositories;
using Utility;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private ICryptoManager cryptoManager;

        public UserService(IUserRepository userRepository, ICryptoManager cryptoManager)
        {
            this.userRepository = userRepository;
            this.cryptoManager = cryptoManager;
        }

        public void Register(string login, string password, string city)
        {
            var user = new User { Login = login, Password = cryptoManager.GetHash(password), City = city, Role = Role.User};
            userRepository.Add(user);
            userRepository.SaveChanges();
        }

        public bool ValidateUser(string login, string password)
        {
            var user = userRepository.FindUserByLogin(login);
            return user != null && cryptoManager.VarifyHash(user.Password, password);
        }


        public bool IsLoginExist(string login)
        {
            return userRepository.FindUserByLogin(login) != null;
        }

        public User GetUserByLogin(string login)
        {
            return userRepository.FindUserByLogin(login);
        }
    }
}
