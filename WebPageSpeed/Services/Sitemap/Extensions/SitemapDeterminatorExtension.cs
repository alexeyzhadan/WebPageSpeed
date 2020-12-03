using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Services.Sitemap.Interfaces;

namespace WebPageSpeed.Services.Sitemap.Extensions
{
    public static class SitemapDeterminatorExtension
    {
        public static IServiceCollection AddSitemapDeterminator(this IServiceCollection services)
            => services.AddTransient<ISitemapDeterminator, SitemapDeterminatorMock>();
    }
}