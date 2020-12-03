using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using WebPageSpeed.Services.WebPageAnalysis.MonitoringResponseTime.Interfaces;

namespace WebPageSpeed.Services.WebPageAnalysis.MonitoringResponseTime
{
    public class RequestMonitoring : IRequestMonitoring
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestMonitoring(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TimeSpan> GetResponseTimeAsync(string uri)
        {
            var client = _httpClientFactory.CreateClient(StringConstant.MONITORING);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            await client.SendAsync(request);
            var watcher = (Stopwatch)request.Properties[StringConstant.STOPWATCH];
            return watcher.Elapsed;
        }
    }
}