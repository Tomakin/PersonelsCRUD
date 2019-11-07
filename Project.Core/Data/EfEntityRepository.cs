using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.Core.Data
{
    public class EfEntityRepository<T> : IEntityRepository<T>
       where T : class, IEntity, new()
    {
        // Database
        protected readonly DbContext context;

        public EfEntityRepository(DbContext _context)
            => context = _context;

        // Get Single Entity
        public virtual T Get(Expression<Func<T, bool>> filter)
            => context.Set<T>().FirstOrDefault(filter);

        public virtual T GetByID(int ID)
            => context.Set<T>().Find(ID);

        // Get Entity List
        public virtual IQueryable<T> GetAll()
            => context.Set<T>();

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> filter)
            => context.Set<T>().Where(filter);

        // Count
        public int Count(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
                return context.Set<T>().Count();
            return context.Set<T>().Count(filter);
        }

        // Create
        public void Add(T entity)
            => context.Set<T>().Add(entity);

        // Update
        public void Update(T entity)
            => context.Set<T>().Update(entity);

        // Delete
        public void Delete(T entity)
            => context.Set<T>().Remove(entity);

        public void DeleteByID(int ID)
            => context.Set<T>().Remove(this.GetByID(ID));

    }
}
