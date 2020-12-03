using System;
using System.Threading.Tasks;

namespace WebPageSpeed.Services.WebPageAnalysis.MonitoringResponseTime.Interfaces
{
    public interface IRequestMonitoring
    {
        Task<TimeSpan> GetResponseTimeAsync(string uri);
    }
}