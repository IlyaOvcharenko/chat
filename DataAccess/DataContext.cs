using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Data;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DbConnection")
        {
            Database.SetInitializer<DataContext>(new DbInitializer());
        }


        public DbSet<User> Users { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().Property(t => t.Login).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(t => t.City).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(t => t.Password).IsRequired().HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }


    }
}
