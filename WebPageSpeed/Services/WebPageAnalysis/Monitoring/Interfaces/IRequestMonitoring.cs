using System;
using System.Threading.Tasks;

namespace WebPageSpeed.Services.WebPageAnalysis.Monitoring.Interfaces
{
    public interface IRequestMonitoring
    {
        Task<TimeSpan> GetResponseTimeAsync(string uri);
    }
}