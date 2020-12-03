using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Services.WebPageAnalysis.MonitoringResponseTime.Interfaces;

namespace WebPageSpeed.Services.WebPageAnalysis.MonitoringResponseTime.Extensions
{
    public static class MonitoringExtension
    {
        public static IServiceCollection AddMonitoring(this IServiceCollection services)
        {
            services.AddScoped<MonitoringHandler>();
            services.AddHttpClient(StringConstant.MONITORING)
                .AddHttpMessageHandler<MonitoringHandler>();
            services.AddScoped<IRequestMonitoring, RequestMonitoring>();

            return services;
        }
    }
}