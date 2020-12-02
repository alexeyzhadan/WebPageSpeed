using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Services.WebPageAnalysis.Sitemap.Interfaces;

namespace WebPageSpeed.Services.WebPageAnalysis.Sitemap.Extensions
{
    public static class SitemapDeterminatorExtension
    {
        public static IServiceCollection AddSitemapDeterminator(this IServiceCollection services)
            => services.AddTransient<ISitemapDeterminator, SitemapDeterminatorMock>();
    }
}