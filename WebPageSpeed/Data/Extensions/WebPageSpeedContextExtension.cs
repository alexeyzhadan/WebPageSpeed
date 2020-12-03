using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Data.Repositories.Extensions;

namespace WebPageSpeed.Data.Extensions
{
    public static class WebPageSpeedContextExtension
    {
        public static IServiceCollection AddWebPageSpeedContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebPageSpeedContext>(options => options.UseSqlServer(connectionString));
            services.AddRepositories();

            return services;
        }
    }
}