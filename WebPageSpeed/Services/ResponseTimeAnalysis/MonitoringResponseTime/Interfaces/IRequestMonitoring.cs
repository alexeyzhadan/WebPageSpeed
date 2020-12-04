using System;
using System.Threading.Tasks;

namespace WebPageSpeed.Services.ResponseTimeAnalysis.MonitoringResponseTime.Interfaces
{
    public interface IRequestMonitoring
    {
        Task<TimeSpan> GetResponseTimeAsync(Uri uri);
    }
}