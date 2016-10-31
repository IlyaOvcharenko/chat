using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Data;
using Data.Enums;

namespace DataAccess
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        public DbInitializer()
        {

        }
        protected override void Seed(DataContext context)
        {
            var users = new Collection<User>
            {
                new User {Login = "admin", Role = Role.Admin, Password = "33354741122871651676713774147412831195", City = "DefaultCity"},
                new User {Login = "user1", Role = Role.User, Password = "33354741122871651676713774147412831195", City = "DefaultCity"},
                new User {Login = "user2", Role = Role.User, Password = "33354741122871651676713774147412831195", City = "DefaultCity"},
                new User {Login = "user3", Role = Role.User, Password = "33354741122871651676713774147412831195", City = "DefaultCity"},
                new User {Login = "user4", Role = Role.User, Password = "33354741122871651676713774147412831195", City = "DefaultCity"},
                new User {Login = "user5", Role = Role.User, Password = "33354741122871651676713774147412831195", City = "DefaultCity"},
                new User {Login = "user6", Role = Role.User, Password = "33354741122871651676713774147412831195", City = "DefaultCity"}
            };

            
            context.Users.AddRange(users);
            base.Seed(context);
        }
    }
}
