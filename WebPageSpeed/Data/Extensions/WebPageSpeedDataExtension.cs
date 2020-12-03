using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Data.Repositories.Extensions;

namespace WebPageSpeed.Data.Extensions
{
    public static class WebPageSpeedDataExtension
    {
        public static IServiceCollection AddWebPageSpeedData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebPageSpeedContext>(options => options.UseSqlServer(connectionString));
            services.AddRepositories();

            return services;
        }
    }
}