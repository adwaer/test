using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PM.Identity.WebApi.Configuration;
using PM.Identity.WebApi.Middleware;
using SilentNotary.Common;

namespace PM.Identity.WebApi
{
    /// <summary>
    /// Startup config
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    
        /// <summary>
        /// Configurator
        /// </summary>
        public IConfiguration Configuration { get; }
        
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsPolicy()
                .AddHttpContextAccessor()
                .AddAuth(Configuration)
                .AddSwaggerGen()
                .AddIdentity()
                .AddMappings()
                .AddCqrs()
                .AddDDD()
                .AddCommandHandlers()
                .AddQueries()
                .AddSingleton<IDiScope, ServiceLocatior>()
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Config
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMiddleware<ErrorHandlingMiddleware>()
                .UseCors("CorsPolicy")
                .UseMvc()
                .UseAuthentication()
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Promomash Identity API");
                    c.DocumentTitle = "Promomash Identity API Documentation";
                })
                .Run(async (context) => { await context.Response.WriteAsync("The service is online"); });
        }
    }
}