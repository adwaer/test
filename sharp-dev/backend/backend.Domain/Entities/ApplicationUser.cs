using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace backend.Domain.Entities
{
    /// <summary>
    /// App user
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Required, EmailAddress, MaxLength(255), Index("UX_Email", IsUnique = true)]
        public override string Email { get; set; }
    }
}
