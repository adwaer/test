using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SilentNotary.Common;
using SilentNotary.Common.Entity.Uow;
using SilentNotary.Cqrs.Queries;

namespace PM.Identity.Dal
{
    public class IdentityCtxUow : IDataSetUow, ILinqProvider
    {
        private readonly DbContext _dbContext;

        public IdentityCtxUow(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetQuery<T>() where T : class, IHasKey
        {
            return _dbContext.Set<T>()
                .AsNoTracking();
        }

        public IQueryable<T> Include<T, TProp>(IQueryable<T> queryable, params Expression<Func<T, TProp>>[] paths)
            where T : class
        {
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }

            return queryable;
        }

        public object GetContext()
        {
            return this;
        }

        public TEntity Find<TEntity>(object id) where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .Find(id);
        }

        public Task<TEntity> FindAsync<TEntity>(object id) where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .FindAsync(id);
        }

        public void AddEntity<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : class
        {
            _dbContext.Set<T>().AddRange(entity);
        }

        public void RemoveEntity<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entity) where T : class
        {
            _dbContext.Set<T>().RemoveRange(entity);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void FixupState<T>(T entity) where T : class
        {
            var type = entity.GetType();
            var ipProp = type.GetProperty("Id");
            if (!ipProp.PropertyType.IsValueType)
            {
                throw new NotImplementedException();
            }

            if (_dbContext.Entry(entity).State != EntityState.Deleted)
            {
                _dbContext.Entry(entity).State =
                    ipProp.GetValue(entity).Equals(Activator.CreateInstance(ipProp.PropertyType))
                        ? EntityState.Added
                        : EntityState.Modified;
            }
        }
    }
}
