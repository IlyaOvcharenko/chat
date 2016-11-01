using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Base;
using DataAccess.Repositories;

namespace DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected DataContext DataContext { get; }

        public BaseRepository(DataContext dataContext)
        {
            this.DataContext = dataContext;
        }

        public virtual void Dispose()
        {
            DataContext.Dispose();
        }

        public  T Create(T entity)
        {
            DataContext.Set<T>().Add(entity);
            this.SaveChanges();
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return DataContext.Set<T>();
        }

        public T GetById(int id)
        {
            return DataContext.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public void Update(T entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public  void Delete(T entity)
        {
            DataContext.Set<T>().Remove(entity);
            this.SaveChanges();
        }

        protected virtual void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}
