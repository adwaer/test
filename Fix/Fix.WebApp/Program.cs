using System;
using Fix.Dal;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fix.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = BuildWebHost1(args, true);

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					SeedData.Initialize(services);
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred seeding the DB.");
				}
			}

			host.Run();
		}

		private static IWebHost BuildWebHost1(string[] args, bool runHf) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseSetting("RunHangfire", runHf ? "true" : "false")
				.Build();

		public static IWebHost BuildWebHost(string[] args)
		{
			return BuildWebHost1(args, false);
		}
	}
}