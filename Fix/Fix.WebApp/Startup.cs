using AutoMapper;
using Fix.Dal;
using Fix.Domain;
using Fix.Infrastructure;
using Fix.Infrastructure.Services;
using Fix.NangFire;
using Fix.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.WebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			// Add application services.
			services.AddTransient<IDataSetUow, ApplicationDbContext>();
			services.AddTransient<INodesService, NodesService>();
			services.AddSingleton<WebNodesService>();

			if (WithHf)
			{
				var connString = Configuration.GetConnectionString("hangfire.sqlserver");
				services.AddHangfire(x => x.UseSqlServerStorage(connString));
			}

			services.AddMvc();

			services.AddAutoMapper(typeof(Startup));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}
			
			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			if (WithHf)
			{
				app.UseHangfireDashboard("/hangfire", new DashboardOptions());
				app.UseHangfireServer();

				var hangfireActivator = (WebNodesService) app.ApplicationServices.GetService(typeof(WebNodesService));
				hangfireActivator.ActivateJobs();
			}
		}

        private bool WithHf => Configuration["RunHangfire"] == "true";
	}
}