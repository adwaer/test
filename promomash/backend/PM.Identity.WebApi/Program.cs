using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PM.Identity.Dal;

namespace PM.Identity.WebApi
{
    /// <summary>
    /// Endpoint
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start function
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //1. Get the IWebHost which will host this application.
            var host = CreateWebHostBuilder(args);

            //2. Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of BoardGamesDBContext in our services layer
                var services = scope.ServiceProvider;
                using (var context = services.GetRequiredService<IdentityDbCtx>())
                {
                    //4. Call the DataGenerator to create sample data
                    IdentityDbCtx.Initialize(context);
                }
            }

            //Continue to run the application
            host.Run();
        }

        /// <summary>
        /// App builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();
    }
}