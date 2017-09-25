using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend.Domain.Entities;
using In.Entity.Uow;
using Microsoft.AspNet.Identity.EntityFramework;

namespace backend.Dal
{
    public class Ctx : IdentityDbContext<ApplicationUser>, IDataSetUow
    {
        public Ctx()
            : base("ctx")
        {
        }

        public DbSet<MessageHistory> MessageHistories { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }
        public DbSet<FrequentlyPwd> FrequentlyPwds { get; set; }

        public static IdentityDbContext<ApplicationUser> Create()
        {
            return new Ctx();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }

        public IQueryable Query(Type type)
        {
            return Set(type);
        }

        public IQueryable<T> Include<T, TProp>(IQueryable<T> queryable, params Expression<Func<T, TProp>>[] paths) where T : class
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
            return Set<TEntity>()
                .Find(id);
        }

        public Task<TEntity> FindAsync<TEntity>(object id) where TEntity : class
        {
            return Set<TEntity>()
                .FindAsync(id);
        }

        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : class
        {
            Set<T>().AddRange(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entity) where T : class
        {
            Set<T>().RemoveRange(entity);
        }

        public int Commit()
        {
            return SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return SaveChangesAsync();
        }

        public void FixupState<T>(T entity) where T : class
        {
            var type = entity.GetType();
            var ipProp = type.GetProperty("Id");
            if (!ipProp.PropertyType.IsValueType)
            {
                throw new NotImplementedException();
            }

            if (Entry(entity).State != EntityState.Deleted)
            {
                Entry(entity).State = ipProp.GetValue(entity).Equals(Activator.CreateInstance(ipProp.PropertyType)) ?
                    EntityState.Added :
                    EntityState.Modified;
            }
        }
    }
}
