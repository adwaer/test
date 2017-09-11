using System;
using Autofac;
using In.Cqrs.Query;
using In.Cqrs.Query.Impls;

namespace backend.WebApi.config
{
    public class QueryAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterType<QueryBuilder>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<QueryFactory>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(QueryFor<>))
                .As(typeof(IQueryFor<>))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            IocConfig.RegisterAssemblyImplementations(builder, assemblies, typeof(IQuery<,>))
                .InstancePerLifetimeScope();

        }

    }
}