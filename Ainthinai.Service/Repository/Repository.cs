using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ainthinai.Service.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _dbContext.AddAsync<T>(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Remove<T>(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return  _dbSet.AsEnumerable<T>();
        }

        public async Task<T> Get(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where<T>(predicate).AsEnumerable();
        }

        public async Task Update(T entity)
        {
            var records = _dbSet.AsNoTracking().Where(o => o == entity);
            if (records != null)
            {
                _dbSet.Update(entity);
                //_dbContext.Entry<T>(entity).State = EntityState.Modified;
                //_dbContext.Attach<T>(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
