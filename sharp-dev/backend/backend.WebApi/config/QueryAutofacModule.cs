using System;
using Autofac;
using backend.CommandQuery;
using In.Cqrs.Query;
using In.Cqrs.Query.Impls;

namespace backend.WebApi.config
{
    /// <summary>
    /// Query module
    /// </summary>
    public class QueryAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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

            builder.RegisterType<ExpressionQuery>()
                .AsSelf()
                .InstancePerLifetimeScope();

            var assemblies = new[] { typeof(WorkingDayQuery).Assembly };

            IocConfig.RegisterAssemblyImplementations(builder, assemblies, typeof(IQuery<,>))
                .InstancePerLifetimeScope();
        }

    }
}