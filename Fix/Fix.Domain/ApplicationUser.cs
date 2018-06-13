using Fix.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;

namespace Fix.Domain
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser, IHasKey<string>
	{
	}
}