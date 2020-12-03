using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Data.Repositories.Interface;

namespace WebPageSpeed.Data.Repositories.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IIdentifiableEntityRepository<>), typeof(IdentifiableEntityRepository<>));
            services.AddScoped<IWebPageRepository, WebPageRepository>();

            return services;
        }
    }
}