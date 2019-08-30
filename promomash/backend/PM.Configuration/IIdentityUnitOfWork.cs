using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PM.Models;

namespace PM.Configuration
{
    /// <summary>
    /// Interface unit of work for identity.
    /// </summary>
    public interface IIdentityUnitOfWork
    {
        /// <summary>
        /// Saves all changes made in this context to the database. 
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Provides the APIs for managing user in a persistence store. 
        /// </summary>
        UserManager<Customer> UserManager { get; }

        /// <summary>
        /// Provides the APIs for user sign in. 
        /// </summary>
        SignInManager<Customer> SignInManager { get; }
    }
}