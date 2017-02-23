using System;
using Microsoft.Extensions.DependencyInjection;

namespace ResolveByName.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider, string name)
            where TService : class
        {

            return serviceProvider.GetServiceNamesMap().Resolve<TService>(serviceProvider, name);
        }

        public static object GetService(this IServiceProvider serviceProvider, Type serviceType, string name)
        {
            return serviceProvider.GetServiceNamesMap().Resolve(serviceProvider, serviceType, name);
        }

        private static ServiceNamesMap GetServiceNamesMap(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<ServiceNamesMap>();
        }
    }
}