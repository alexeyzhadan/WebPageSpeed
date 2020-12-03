using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebPageSpeed.Services.ResponseTimeAnalysis.MonitoringResponseTime
{
    public class MonitoringHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var watcher = new Stopwatch();

            request.Properties.Add(
                new KeyValuePair<string, object>(StringConstant.STOPWATCH, watcher));

            watcher.Start();
            var response = await base.SendAsync(request, cancellationToken);
            watcher.Stop();

            return response;
        }
    }
}