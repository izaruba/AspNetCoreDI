using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Environments
{
    public class Startup
    {
        private readonly IHostingEnvironment env;

        public Startup(IHostingEnvironment env)
        {
            this.env = env;
        }

        /// Change ASPNETCORE_ENVIRONMENT value in <see cref="~/Properties/launchSettings.json" /> to Development, Staging or Production
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            
            if (this.env.IsDevelopment())
            {
                services.AddScoped<IService, LocalhostService>();
            }
            else
            {
                services.AddScoped<IService, CloudService>();
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }
    }
}