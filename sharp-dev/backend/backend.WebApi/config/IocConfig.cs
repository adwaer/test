using System;
using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Autofac.Integration.WebApi;
using backend.Dal;
using backend.Domain.Entities;
using backend.Identity;
using In.Cqrs;
using In.Cqrs.Command;
using In.Cqrs.Storage;
using In.Di;
using In.Entity.Uow;
using Microsoft.AspNet.Identity.Owin;

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
                .As<IDataSetUow>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DependencyResolverDiContainer>()
                .As<IDiScope>()
                .SingleInstance();

            builder.RegisterType<SimpleMsgBus>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SimpleStorage<>))
                .As(typeof(IStorage<>))
                .AsSelf()
                .SingleInstance();
            RegisterAssemblyImplementations(builder, assemblies, typeof(IStorage<>))
                .SingleInstance();

            builder.RegisterType<MessageHistory>()
                .As<IMessageResult>()
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(SaveCommandHandler<>))
                .AsSelf()
                .InstancePerLifetimeScope();

            RegisterAssemblyImplementations(builder, assemblies, typeof(IMsgHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterApiControllers(Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly())
                .PropertiesAutowired();

            builder.RegisterAssemblyModules(assemblies);

            builder.RegisterWebApiFilterProvider(httpConfiguration);

            var container = builder.Build();
            return container;
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