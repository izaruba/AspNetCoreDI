using System;

namespace ResolveByName.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider, string name)
            where TService : class
        {
            return ServiceNamesMap.Resolve<TService>(serviceProvider, name);
        }

        public static object GetService(this IServiceProvider serviceProvider, Type serviceType, string name)
        {
            return ServiceNamesMap.Resolve(serviceProvider, serviceType, name);
        }
    }
}