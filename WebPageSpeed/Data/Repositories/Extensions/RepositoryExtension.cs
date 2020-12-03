using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Data.Repositories.Interface;

namespace WebPageSpeed.Data.Repositories.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWebSiteRepository, WebSiteRepository>();
            services.AddScoped<IWebPageRepository, WebPageRepository>();

            return services;
        }
    }
}