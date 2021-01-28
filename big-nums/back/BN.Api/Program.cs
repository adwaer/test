using System;
using System.Linq;
using BN.Dal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BN.Api
{
    /// <summary>
    /// Endpoint
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        /// <summary>
        /// Main method, app starter
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build();

            if (args.Any(arg => arg.Equals("--migrate-db")))
            {
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
                Console.WriteLine("EF MIGRATION SOCCEED");
            }
            else
            {
                host.Run();
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}