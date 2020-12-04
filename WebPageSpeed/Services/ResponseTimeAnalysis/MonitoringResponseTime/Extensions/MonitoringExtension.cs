using Microsoft.Extensions.DependencyInjection;
using WebPageSpeed.Services.ResponseTimeAnalysis.MonitoringResponseTime.Interfaces;

namespace WebPageSpeed.Services.ResponseTimeAnalysis.MonitoringResponseTime.Extensions
{
    public static class MonitoringExtension
    {
        public static IServiceCollection AddMonitoring(this IServiceCollection services)
        {
            services.AddScoped<MonitoringHandler>();
            services.AddHttpClient(
                    StringConstant.MONITORING, 
                    options => options.DefaultRequestHeaders.ConnectionClose = true)
                .AddHttpMessageHandler<MonitoringHandler>();
            services.AddScoped<IRequestMonitoring, RequestMonitoring>();

            return services;
        }
    }
}