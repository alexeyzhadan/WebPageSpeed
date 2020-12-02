using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPageSpeed.Services.Monitoring.Extensions;
using WebPageSpeed.Services.Sitemap.Extensions;

namespace WebPageSpeed
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSitemapDeterminator();
            services.AddMonitoring();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}