using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF_HomeWork_4_CORE
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context) 
        {
            DbSet<T> set = context.Set<T>();
            _dbSet = set;
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
	        return entity;
        }

        public void Delete(int entityId)
        {
            var unit = _dbSet.FirstOrDefault(u => u.Id == entityId);
            _dbSet.Remove(unit);
        }

        public IEnumerable<T> Get()
        {
            return _dbSet;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
