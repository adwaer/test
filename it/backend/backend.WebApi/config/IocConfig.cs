using System;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Autofac.Integration.WebApi;
using backend.Dal;
using In.Cqrs;

namespace backend.WebApi.config
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class IocConfig
    {
        public static IContainer Configure(HttpConfiguration httpConfiguration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var builder = new ContainerBuilder();
            builder
                .RegisterType<Ctx>()
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DependencyResolverDiContainer>()
                .As<IDiScope>()
                .SingleInstance();

            builder.RegisterApiControllers(Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly())
                .PropertiesAutowired();

            builder.RegisterAssemblyModules(assemblies);
            builder.RegisterWebApiFilterProvider(httpConfiguration);

            return builder.Build();
        }

        internal static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAssemblyImplementations(ContainerBuilder builder, Assembly[] assemblies, Type type)
        {
            return builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(type)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}