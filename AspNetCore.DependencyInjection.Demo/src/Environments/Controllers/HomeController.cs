using Microsoft.AspNetCore.Mvc;
using Services;

namespace Environments.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IService service)
        {
            return this.View(model: service.GetType().Name);
        }
    }
}