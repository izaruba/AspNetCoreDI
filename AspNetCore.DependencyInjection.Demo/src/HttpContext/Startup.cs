using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace HttpContext
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<LocalhostService>();
            services.AddScoped<CloudService>();

            services.AddScoped(serviceProvider => {

                var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;

                if (httpContext == null)
                {
                    // Разрешение сервиса происходит не в рамках HTTP запроса
                    return null;
                }

                //// Можно использовать любые данные запроса

                //var queryString = httpContext.Request.Query;

                //return queryString.ContainsKey("useLocal")
                //    ? serviceProvider.GetService<LocalhostService>() as IService
                //    : serviceProvider.GetService<CloudService>() as IService;

                //if (httpContext.Request.ContentType == "text/html")
                //{
                //    var formValues = httpContext.Request.Form;
                //}

                var requestHeaders = httpContext.Request.Headers;

                return requestHeaders.ContainsKey("Use-local")
                    ? serviceProvider.GetService<LocalhostService>() as IService
                    : serviceProvider.GetService<CloudService>() as IService;
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}