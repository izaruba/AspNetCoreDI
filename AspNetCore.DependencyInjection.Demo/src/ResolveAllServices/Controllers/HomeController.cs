using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ResolveAllServices.Services;

namespace ResolveAllServices.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService service;

        public HomeController(IServiceProvider iocContainer)
        {
            var services = iocContainer.GetServices<IService>();

            this.service = services.FirstOrDefault(svc => svc.Name == "cloud");
        }
        public IActionResult Index()
        {
            return this.View(model: $"{this.service?.GetType().Name} #{this.service?.GetHashCode()}");
        }
    }
}