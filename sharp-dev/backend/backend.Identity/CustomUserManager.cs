using backend.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace backend.Identity
{
    /// <summary>
    /// asp.net identity user manager
    /// </summary>
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager(CustomUserStore store)
            : base(store)
        {
            UserTokenProvider = new TotpSecurityStampBasedTokenProvider<ApplicationUser, string>();
            UserValidator = new UserValidator<ApplicationUser, string>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        

        /// <summary>
        /// static factory method which is used for instantiation of the class
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context">owin context</param>
        /// <returns></returns>
        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        {
            return new CustomUserManager(new CustomUserStore(context.Get<IdentityDbContext<ApplicationUser>>()));
        }
    }
}
