using System;
using Microsoft.Extensions.DependencyInjection;

namespace ResolveByName.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceNamesMap.RegisterType<TService, TImplementation>(name);

            return services.AddScoped<TImplementation>();
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, Type service, Type implementation, string name)
        {
            ServiceNamesMap.RegisterType(service, implementation, name);

            return services.AddScoped(implementation);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceNamesMap.RegisterType<TService, TImplementation>(name);

            return services.AddTransient<TImplementation>();
        }

        public static IServiceCollection AddTransient(this IServiceCollection services, Type service, Type implementation, string name)
        {
            ServiceNamesMap.RegisterType(service, implementation, name);

            return services.AddTransient(implementation);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, string name)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceNamesMap.RegisterType<TService, TImplementation>(name);

            return services.AddSingleton<TImplementation>();
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type service, Type implementation, string name)
        {
            ServiceNamesMap.RegisterType(service, implementation, name);

            return services.AddSingleton(implementation);
        }
    }
}