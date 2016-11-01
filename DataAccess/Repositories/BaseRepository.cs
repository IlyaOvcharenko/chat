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

        public void Dispose()
        {
            DataContext.Dispose();
        }

        public T Add(T entity)
        {
            return DataContext.Set<T>().Add(entity);
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
        }

        public  void Delete(T entity)
        {
            DataContext.Set<T>().Remove(entity);
        }

        public virtual void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}
