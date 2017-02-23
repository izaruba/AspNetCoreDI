using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ResolveByName.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            return services.Add(typeof(TService), typeof(TImplementation), ServiceLifetime.Scoped, name);
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, Type service, Type implementation, string name)
        {
            return services.Add(service, implementation, ServiceLifetime.Scoped, name);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            return services.Add(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient, name);
        }

        public static IServiceCollection AddTransient(this IServiceCollection services, Type service, Type implementation, string name)
        {
            return services.Add(service, implementation, ServiceLifetime.Transient, name);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            return services.Add(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton, name);
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type service, Type implementation, string name)
        {
            return services.Add(service, implementation, ServiceLifetime.Singleton, name);
        }

        private static IServiceCollection Add(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime, string name)
        {
            var serviceMap = services.GetOrCreateServiceNamesMap();

            serviceMap.RegisterType(serviceType, implementationType, name);

            services.TryAddSingleton(serviceMap);
            services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));

            return services;
        }

        private static ServiceNamesMap GetOrCreateServiceNamesMap(this IServiceCollection services)
        {
            return services.FirstOrDefault(descriptor => 
                descriptor.ServiceType == typeof(ServiceNamesMap))?.ImplementationInstance as ServiceNamesMap
                ?? new ServiceNamesMap();
        }
    }
}