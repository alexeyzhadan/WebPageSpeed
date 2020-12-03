using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Services.ResponseTimeAnalysis.Interface;
using WebPageSpeed.Services.ResponseTimeAnalysis.MonitoringResponseTime.Extensions;

namespace WebPageSpeed.Services.ResponseTimeAnalysis.Extensions
{
    public static class ResponseTimeAnalysisExtension
    {
        public static IServiceCollection AddWebPageAnalysis(this IServiceCollection services)
        {
            services.AddMonitoring();
            services.AddScoped<IResponseTimeAnalyzer, ResponseTimeAnalyzer>();

            return services;
        }
    }
}