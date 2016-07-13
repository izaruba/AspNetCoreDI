using HttpContext.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace HttpContext.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IService service, bool? useLocal)
        {
            if (this.Request.IsAjaxRequest())
            {
                return this.Json(service.GetType().Name);
            }

            return this.View(model: service.GetType().Name);
        }
    }
}