using System.Web;
using Autofac;
using Autofac.Integration.Owin;
using In.Di;

namespace backend.WebApi.config
{
    public class DependencyResolverDiContainer : IDiScope
    {
        private readonly IComponentContext _componentContext;

        public DependencyResolverDiContainer(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public T Resolve<T>()
        {
            if (HttpContext.Current == null
                || HttpContext.Current.Request.GetOwinContext() == null
                || HttpContext.Current.Request.GetOwinContext().GetAutofacLifetimeScope() == null)
            {
                return _componentContext.Resolve<T>();
            }

            return HttpContext.Current.Request.GetOwinContext().GetAutofacLifetimeScope().Resolve<T>();
        }

    }
}