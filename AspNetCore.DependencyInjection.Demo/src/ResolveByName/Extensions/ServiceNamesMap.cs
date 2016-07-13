using System;
using System.Collections.Generic;

namespace ResolveByName.Extensions
{
    internal class ServiceNamesMap
    {
        /* Key = Service type, value = services names and implementation types */
        private static readonly Dictionary<Type, Dictionary<string, Type>> ServiceNameMap =
            new Dictionary<Type, Dictionary<string, Type>>();

        public static int Count => ServiceNameMap.Count;

        public static void RegisterType<TService, TImplementation>(string name)
            where TImplementation : TService
        {
            RegisterType(typeof(TService), typeof(TImplementation), name);
        }

        public static void RegisterType(Type service, Type implementation, string name)
        {
            if (ServiceNameMap.ContainsKey(service))
            {
                var serviceNames = ServiceNameMap[service];

                if (serviceNames.ContainsKey(name))
                {
                    /* overwrite existing name implementation */
                    serviceNames[name] = implementation;
                }
                else
                {
                    serviceNames.Add(name, implementation);
                }
            }
            else
            {
                ServiceNameMap.Add(service, new Dictionary<string, Type>
                {
                    [name] = implementation
                });
            }
        }

        public static object Resolve(IServiceProvider serviceProvider, Type serviceType, string name)
        {
            var service = serviceType;

            if (service.IsGenericType)
            {
                return ResolveGeneric(serviceProvider, serviceType, name);
            }

            var serviceExists = ServiceNameMap.ContainsKey(service);
            var nameExists = serviceExists && ServiceNameMap[service].ContainsKey(name);

            /* Return `null` if there is no mapping for either service type or requested name */
            if (!(serviceExists && nameExists))
            {
                return null;
            }

            return serviceProvider.GetService(ServiceNameMap[service][name]);
        }

        public static TService Resolve<TService>(IServiceProvider serviceProvider, string name)
            where TService : class
        {
            return Resolve(serviceProvider, typeof(TService), name) as TService;
        }

        private static object ResolveGeneric(IServiceProvider serviceProvider, Type serviceType, string name)
        {
            var genericType = serviceType.GetGenericTypeDefinition();

            var serviceExists = ServiceNameMap.ContainsKey(genericType);
            var nameExists = serviceExists && ServiceNameMap[genericType].ContainsKey(name);

            /* Return `null` if there is no mapping for either service type or requested name */
            if (!(serviceExists && nameExists))
            {
                return null;
            }

            var implementation = ServiceNameMap[genericType][name].MakeGenericType(serviceType.GenericTypeArguments);

            return serviceProvider.GetService(implementation);
        }
    }
}