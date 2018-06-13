using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fix.Domain;
using Fix.Infrastructure;
using Fix.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fix.Dal
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataSetUow
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<WebNode> WebNodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public IQueryable<T> Query<T>() where T : class, IHasKey
        {
            return Set<T>()
                .AsNoTracking();
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

        public void AddEntity<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : class
        {
            Set<T>().AddRange(entity);
        }

        public void RemoveEntity<T>(T entity) where T : class
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
                Entry(entity).State = ipProp.GetValue(entity).Equals(Activator.CreateInstance(ipProp.PropertyType))
                    ? EntityState.Added
                    : EntityState.Modified;
            }
        }
    }
}
