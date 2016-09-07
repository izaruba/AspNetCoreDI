using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ResolveAllServices.Services;

namespace ResolveAllServices.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService service;

        public HomeController(IEnumerable<IService> services)
        {
            this.service = services.FirstOrDefault(svc => svc.Name == "cloud");
        }
        public IActionResult Index()
        {
            return this.View(model: $"{this.service?.GetType().Name} #{this.service?.GetHashCode()}");
        }
    }
}