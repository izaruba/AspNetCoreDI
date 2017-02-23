using System;
using Microsoft.AspNetCore.Mvc;
using ResolveByName.Extensions;
using ResolveByName.MultipleContainers.Attributes;
using Services;

namespace ResolveByName.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceProvider serviceProvider;

        public HomeController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Local()
        {
            var service = this.serviceProvider.GetService<IService>("local");

            return this.View(model: service.GetType().Name);
        }

        public IActionResult Cloud()
        {
            var service = this.serviceProvider.GetService<IService>("cloud");

            return this.View(model: service.GetType().Name);
        }

        public IActionResult LocalWithAttribute([FromNamedServices("local")] IService service)
        {
            return this.View("Local", model: service.GetType().Name);
        }

        public IActionResult CloudWithAttribute([FromNamedServices("cloud")] IService service)
        {
            return this.View("Cloud", model: service.GetType().Name);
        }
    }
}