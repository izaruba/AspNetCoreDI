using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ResolveAllServices.Services;
using System.Linq;

namespace ResolveAllServices
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IService, LocalhostService>();
            services.AddSingleton<IService, CloudService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}