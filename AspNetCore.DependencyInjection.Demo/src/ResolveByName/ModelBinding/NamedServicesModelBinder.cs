using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ResolveByName.Extensions;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ResolveByName.ModelBinding
{
    public class NamedServicesModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            return Task.Run(() =>
            {
                if (bindingContext == null)
                    throw new ArgumentNullException(nameof(bindingContext));

                var serviceName = GetServiceName(bindingContext);

                if (serviceName == null)
                    return Task.FromResult(ModelBindingResult.Failed());

                var serviceProvider = bindingContext.HttpContext.RequestServices;
                var model = serviceProvider.GetService(bindingContext.ModelType, serviceName);

                bindingContext.Model = model;
                bindingContext.ValidationState[model] = new ValidationStateEntry {SuppressValidation = true};
                bindingContext.Result = ModelBindingResult.Success(model);

                return Task.CompletedTask;
            });
        }

        private static string GetServiceName(ModelBindingContext bindingContext)
        {
            var parameter = (ControllerParameterDescriptor)bindingContext
                .ActionContext
                .ActionDescriptor
                .Parameters
                .FirstOrDefault(p => p.Name == bindingContext.FieldName);

            var fromServicesAttribute = parameter
                ?.ParameterInfo
                .GetCustomAttributes(typeof(FromServicesAttribute), false)
                .FirstOrDefault() as FromServicesAttribute;

            return fromServicesAttribute?.ServiceName;
        }
    }
}