using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ResolveByName.MultipleContainers.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromNamedServicesAttribute : ModelBinderAttribute
    {
        public FromNamedServicesAttribute(string serviceName)
        {
            this.ServiceName = serviceName;
            this.BinderType = typeof(NamedServicesModelBinder);
        }
        public string ServiceName { get; set; }
        public override BindingSource BindingSource => BindingSource.Services;
    }
}