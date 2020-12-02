using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Services.WebPageAnalysis.Monitoring.Extensions;
using WebPageSpeed.Services.WebPageAnalysis.Sitemap.Extensions;

namespace WebPageSpeed.Services.WebPageAnalysis.Extensions
{
    public static class WebPageAnalysisExtensions
    {
        public static IServiceCollection AddWebPageAnalysis(this IServiceCollection services)
        {
            services.AddSitemapDeterminator();
            services.AddMonitoring();
            services.AddScoped<SpeedAnalyzer>();

            return services;
        }
    }
}