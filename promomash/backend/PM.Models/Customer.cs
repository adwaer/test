using Microsoft.AspNetCore.Identity;
using SilentNotary.Common;

namespace PM.Models
{
    public class Customer : IdentityUser, IHasKey<string>
    {
        public string Salt { get; set; }
        public override string Email { get; set; }
        public override string UserName { get; set; }
        public override string PasswordHash { get; set; }
    }
}