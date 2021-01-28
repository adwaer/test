using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BN.CommandHandlers;
using BN.Dal;
using BN.Domain.Models;
using In.Common.Config;
using In.Cqrs.Command.Simple.Config;
using In.Cqrs.Query.Simple.Config;
using In.DataAccess.EfCore.Config;
using In.DataMapping.Automapper.Config;
using In.DDD.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BN.Api.Config
{
    /// <summary>
    /// Config extensions
    /// </summary>
    public static class IocExtensions
    {
        /// <summary>
        /// Adds common services
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            var builder = new CommonModuleBuilder(services);
            return builder.AddServices();
        }

        /// <summary>
        /// Add command sender and handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqrsSimpleCommands(this IServiceCollection services)
        {
            var builder =
                new SimpleCommandModuleBuilder<SimpleMessageResult>(services, typeof(CommandHandlersCfg).Assembly);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add query sender and handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqrsSimpleQueries(this IServiceCollection services)
        {
            var builder = new SimpleQueryModuleBuilder(services);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add DDD
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDdd(this IServiceCollection services)
        {
            var builder = new DddModuleBuilder(services);
            return builder.AddServices();
        }

        /// <summary>
        /// Add ef core providers
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddEfDb(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("ctx");

            var builder = new DataAccessEfCoreModuleBuilder<DbCtx>(services, typeof(DbCtx).Assembly);
            return builder
                .AddServices()
                .AddDbContext<DbCtx>(x => x.UseSqlServer(conn));
        }

        /// <summary>
        /// Add automapper mappings
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddDMAutomapper(this IServiceCollection services)
        {
            var builder = new DataMappingAutomapperModuleBuilder(services,
                typeof(IocExtensions).Assembly);
            return builder.AddServices();
        }

        /// <summary>
        /// cons for CORS policy
        /// </summary>
        public const string CorsPolicy = "CorsPolicy";

        /// <summary>
        /// Cors policy
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
        {
            var domains = config["Cors:Domains"].Split(';', ',');
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder => builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(domains)
                        .AllowCredentials());
            });

            return services;
        }

        /// <summary>
        /// Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Example API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Unhandled error handler
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IApplicationBuilder UseUnhandledErrorLogger(this IApplicationBuilder builder,
            ILoggerFactory loggerFactory)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger("UnhandledExceptionLogger");
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                logger.LogCritical(e.Exception.Flatten(), "Unobserved task exception, {exceptionType}",
                    e.Exception.GetType());
                e.SetObserved();
            };
            AppDomain.CurrentDomain.UnhandledException +=
                (s, e) => logger.LogCritical(e.ExceptionObject as Exception, "Unhandled exception, {exceptionType}",
                    e.ExceptionObject.GetType());

            return builder;
        }
    }
}