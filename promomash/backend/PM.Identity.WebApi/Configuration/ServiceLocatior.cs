using System;
using Microsoft.Extensions.DependencyInjection;
using SilentNotary.Common;

namespace PM.Identity.WebApi.Configuration
{
    /// <summary>
    /// Service locator for exceptional situations
    /// </summary>
    public class ServiceLocatior : IDiScope
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ServiceLocatior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Generic resolve
        /// </summary>
        /// <typeparam name="TSvc"></typeparam>
        /// <returns></returns>
        public TSvc Resolve<TSvc>()
        {
            return _serviceProvider.GetRequiredService<TSvc>();
        }
        
        /// <summary>
        /// Typed resolve 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}