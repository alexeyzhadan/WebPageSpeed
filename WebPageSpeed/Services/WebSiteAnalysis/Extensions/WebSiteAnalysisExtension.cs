using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Services.WebSiteAnalysis.Interface;

namespace WebPageSpeed.Services.WebSiteAnalysis.Extensions
{
    public static class WebSiteAnalysisExtension
    {
        public static IServiceCollection AddWebSiteAnalyzer(this IServiceCollection services)
            => services.AddScoped<IWebSiteAnalyzer, WebSiteAnalyzer>();
    }
}