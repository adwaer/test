using Autofac;
using backend.CommandQuery;
using In.Cqrs;
using In.Cqrs.Command;

namespace backend.WebApi.config
{
    /// <summary>
    /// commands module
    /// </summary>
    public class CommandsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = new[] { typeof(MovementCommandHandler).Assembly };

            builder.RegisterGeneric(typeof(SaveCommandHandler<>))
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DeleteCommandHandler<>))
                .AsSelf()
                .InstancePerLifetimeScope();

            IocConfig.RegisterAssemblyImplementations(builder, assemblies, typeof(IMsgHandler<>))
                .InstancePerLifetimeScope();
        }
    }
}
