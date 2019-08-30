using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PM.CommandHandlers;
using PM.Configuration;
using PM.Identity.Dal;
using PM.Infrastructure.Dal;
using PM.Models;
using PM.QueryHandlers;
using SilentNotary.Common;
using SilentNotary.Common.Entity.Uow;
using SilentNotary.Common.Mapping;
using SilentNotary.Common.Storage;
using SilentNotary.Cqrs.Domain;
using SilentNotary.Cqrs.Domain.Interfaces;
using SilentNotary.Cqrs.EFCore;
using SilentNotary.Cqrs.Queries;
using SilentNotary.Cqrs.Queries.Impls;
using Swashbuckle.AspNetCore.Swagger;

namespace PM.Identity.WebApi.Configuration
{
    /// <summary>
    /// Ioc extensions
    /// </summary>
    public static class IocConfig
    {
        private static Assembly[] Assemblies = {
            typeof(MessageHistory).Assembly,
            typeof(MessageHistoryStorage).Assembly,
            typeof(MsgHandlerErrorDecorator<>).Assembly,
            typeof(UserTokenQueryHandler).Assembly,
        };

        /// <summary>
        /// Swagger generator
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Promomash Identity API", Version = "v1"});
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PM.Identity.WebApi.xml"));
                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });
        }

        /// <summary>
        /// CORD allow all
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        /// <summary>
        /// Cqrs config
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            return services.AddScoped<IMessageSender, SimpleMsgBus>()
                .AddSingleton(typeof(IStorage<>), typeof(SimpleStorage<>))
                .RegisterAssemblyImplementationsScoped(Assemblies, typeof(IStorage<>))
                .AddTransient<IMessageResult, MessageHistory>()
                .AddScoped<IQueryBuilder, QueryBuilder>()
                .AddScoped<IQueryFactory, QueryFactory>()
                .AddSingleton(typeof(IQueryFor<>), typeof(QueryFor<>))
                .AddTransient(typeof(IGenericQueryBuilder<>), typeof(GenericQueryBuilder<>))
                .Scan(scan => scan
                    .FromAssemblies(Assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(GenericQueryBuilder<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );
        }
        
        /// <summary>
        /// DDD config
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDDD(this IServiceCollection services)
        {
            return services.AddScoped<IDataSetUow, IdentityCtxUow>()
                .AddScoped<DbContext, IdentityDbCtx>()
                .AddScoped<IDddUnitOfWork, DddUnitOfWork>()
                .AddScoped<IValueObjectProvider, ValueObjectProvider>()
                .RegisterAssemblyImplementationsScoped(Assemblies, typeof(IAggregateFactory))
                .AddScoped<ILinqProvider, IdentityCtxUow>()
                .AddScoped<IEventDispatcher, EventDispatcher>()
                .AddScopedGenerics(Assemblies, typeof(AggregateRepository<,>),
                    new[] {typeof(AggregateRepositoryTrackDecorator<,>)});
        }
        
        /// <summary>
        /// Cqrs command handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            return services
                .AddScopedGenerics(Assemblies, typeof(ICommandHandler<>))
                .AddScopedGenerics(Assemblies, typeof(ICommandHandler<,>))
                .AddScoped(typeof(IMsgHandler<>), typeof(MsgHandlerErrorDecorator<>))
                .AddScoped(typeof(IMsgHandler<,>), typeof(MsgHandlerErrorDecorator<,>));
        }
        
        /// <summary>
        /// Cqrs query handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services.RegisterAssemblyImplementationsScoped(Assemblies, typeof(IQuery<,>));
        }
        
        /// <summary>
        /// Auth config
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuth(this IServiceCollection services,
            IConfiguration configuration)
        {
            var section = configuration.GetSection("AuthenticationOptions");
            var authenticationOptions = section.Get<AuthenticationConfig>();

            services
                .Configure<AuthenticationConfig>(configuration.GetSection("AuthenticationOptions"))
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authenticationOptions.Url,
                        ValidateAudience = true,
                        ValidAudience = authenticationOptions.Url,
                        ValidateLifetime = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.SecretJwtKey)),
                    };
                });

            return services;
        }
        
        /// <summary>
        /// Automapper config
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddScoped<MapperServiceFactory>();

            var mappingProfiles = Assemblies.SelectMany(a =>
                a.DefinedTypes.Where(t => t.BaseType == typeof(Profile) && !t.IsAbstract && t.IsPublic));

            foreach (var mappingProfile in mappingProfiles)
            {
                services.AddTransient(typeof(Profile), mappingProfile);
            }

            return services.AddSingleton<AutoMapper.IConfigurationProvider>(provider => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in provider.GetService<IEnumerable<Profile>>())
                        cfg.AddProfile(profile);
                }))
                .AddAutoMapper();
        }
        
        /// <summary>
        /// Asp.net Identity config
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<Customer>, PasswordHasherWithSalt>()
                .AddIdentity<Customer, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 2;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = true;
                })
                .AddEntityFrameworkStores<IdentityDbCtx>()
                .AddDefaultTokenProviders();

            return services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<IdentityDbCtx>((sp, options) =>
                {
                    options.UseInMemoryDatabase();
                })
                .AddTransient<IIdentityUnitOfWork, IdentityUnitOfWork>()
                .AddTransient<UserManager<Customer>>()
                .AddTransient<SignInManager<Customer>>();
        }
    }
}