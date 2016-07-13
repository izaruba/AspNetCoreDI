using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ResolveByName.ModelBinding
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromServicesAttribute : ModelBinderAttribute
    {
        public FromServicesAttribute(string serviceName)
        {
            this.ServiceName = serviceName;
            this.BinderType = typeof(NamedServicesModelBinder);
        }

        public string ServiceName { get; set; }

        public override BindingSource BindingSource => BindingSource.Services;
    }
}