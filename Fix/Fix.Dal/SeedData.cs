using System;
using Fix.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Dal
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new ApplicationDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
			{
				context.Database.Migrate();
				
				// Look for any movies.
				if (!context.WebNodes.Any())
				{
					context.WebNodes.Add(
						new WebNode
						{
							Interval = 10,
							IsAvailable = true,
							Url = "https://google.com",
							Name = "google"
						});
					context.SaveChanges();
				}
			}
		}
	}
}