using System;
using System.Collections.Generic;
using System.Reflection;

namespace ResolveByName.Extensions
{
    internal class ServiceNamesMap
    {
        /* Key = Service type, value = services names and implementation types */
        private readonly Dictionary<Type, Dictionary<string, Type>> serviceNameMap =
            new Dictionary<Type, Dictionary<string, Type>>();

        public int Count => this.serviceNameMap.Count;

        public void RegisterType<TService, TImplementation>(string name)
            where TImplementation : TService
        {
            this.RegisterType(typeof(TService), typeof(TImplementation), name);
        }

        public void RegisterType(Type service, Type implementation, string name)
        {
            if (this.serviceNameMap.ContainsKey(service))
            {
                var serviceNames = this.serviceNameMap[service];

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
                this.serviceNameMap.Add(service, new Dictionary<string, Type>
                {
                    [name] = implementation
                });
            }
        }

        public object Resolve(IServiceProvider serviceProvider, Type serviceType, string name)
        {
            var service = serviceType;

            if (service.GetTypeInfo().IsGenericType)
            {
                return this.ResolveGeneric(serviceProvider, serviceType, name);
            }

            var serviceExists = this.serviceNameMap.ContainsKey(service);
            var nameExists = serviceExists && this.serviceNameMap[service].ContainsKey(name);

            /* Return `null` if there is no mapping for either service type or requested name */
            if (!(serviceExists && nameExists))
            {
                return null;
            }

            return serviceProvider.GetService(this.serviceNameMap[service][name]);
        }

        public TService Resolve<TService>(IServiceProvider serviceProvider, string name)
            where TService : class
        {
            return this.Resolve(serviceProvider, typeof(TService), name) as TService;
        }

        private object ResolveGeneric(IServiceProvider serviceProvider, Type serviceType, string name)
        {
            var genericType = serviceType.GetGenericTypeDefinition();

            var serviceExists = this.serviceNameMap.ContainsKey(genericType);
            var nameExists = serviceExists && this.serviceNameMap[genericType].ContainsKey(name);

            /* Return `null` if there is no mapping for either service type or requested name */
            if (!(serviceExists && nameExists))
            {
                return null;
            }

            var implementation = this.serviceNameMap[genericType][name].MakeGenericType(serviceType.GenericTypeArguments);

            return serviceProvider.GetService(implementation);
        }
    }
}