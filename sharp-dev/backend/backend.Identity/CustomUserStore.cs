using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace backend.Identity
{
    /// <summary>
    /// asp.net identity user store
    /// </summary>
    public class CustomUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    {
        private readonly DbContext _context;

        public CustomUserStore(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {

        }

        public async Task CreateAsync(ApplicationUser user)
        {
            _context.Set<ApplicationUser>().Add(user);
            user.Id = Guid.NewGuid().ToString();

            _context.Entry(user).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return Task<ApplicationUser>.Factory.StartNew(() => _context.Set<ApplicationUser>().FirstOrDefault(u => u.Id == userId));
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task<ApplicationUser>.Factory.StartNew(() => _context.Set<ApplicationUser>().FirstOrDefault(u => u.Email == userName));
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }
        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(!String.IsNullOrEmpty(user.PasswordHash));
        }
    }
}
