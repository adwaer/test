using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PM.Configuration;
using PM.Models;

namespace PM.Identity.Dal
{
    /// <summary>
    /// Unit of work for identity.
    /// </summary>
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        /// <summary>
        /// Database context used for identity.
        /// </summary>
        private readonly IdentityDbCtx _dbContext;

        /// <summary>
        /// Initializes a new instance of the class. 
        /// </summary>
        /// <param name="dbContext">Database context used for identity.</param>
        /// <param name="userManager">Provides the APIs for managing user in a persistence store. </param>
        /// <param name="signInManager">Provides the APIs for user sign in. </param>
        /// <param name="accountOperationRepository">Repository for working with account operations data.</param>
        public IdentityUnitOfWork(
            IdentityDbCtx dbContext, 
            UserManager<Customer> userManager, 
            SignInManager<Customer> signInManager)
        {
            _dbContext = dbContext;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public UserManager<Customer> UserManager { get; }

        /// <inheritdoc />
        public SignInManager<Customer> SignInManager { get; }

    }
}