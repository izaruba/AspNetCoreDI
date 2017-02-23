using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ResolveByName.Extensions;

namespace ResolveByName.MultipleContainers.Attributes
{
    public class NamedServicesModelBinder : IModelBinder
    {
        private readonly IServiceProvider serviceProvider;

        public NamedServicesModelBinder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var serviceName = GetServiceName(bindingContext);

            if (serviceName == null) return Task.FromResult(ModelBindingResult.Failed());

            var model = this.serviceProvider.GetService(bindingContext.ModelType, serviceName);

            bindingContext.Model = model;
            bindingContext.ValidationState[model] = new ValidationStateEntry { SuppressValidation = true };
            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
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
                .GetCustomAttributes(typeof(FromNamedServicesAttribute), false)
                .FirstOrDefault() as FromNamedServicesAttribute;

            return fromServicesAttribute?.ServiceName;
        }
    }
}