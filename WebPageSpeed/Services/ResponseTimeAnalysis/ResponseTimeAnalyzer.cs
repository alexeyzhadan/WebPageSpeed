using Dasync.Collections;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPageSpeed.Models;
using WebPageSpeed.Services.ResponseTimeAnalysis.Interface;
using WebPageSpeed.Services.ResponseTimeAnalysis.MonitoringResponseTime.Interfaces;

namespace WebPageSpeed.Services.ResponseTimeAnalysis
{
    public class ResponseTimeAnalyzer : IResponseTimeAnalyzer
    {
        private const int NUBMER_OF_ANALYZES = 3;

        private readonly IRequestMonitoring _requestMonitoring;
        private readonly ILogger<ResponseTimeAnalyzer> _logger;

        public ResponseTimeAnalyzer(
            IRequestMonitoring requestMonitoring,
            ILogger<ResponseTimeAnalyzer> logger)
        {
            _requestMonitoring = requestMonitoring;
            _logger = logger;
        }

        public async Task<List<AnalysisOfWebPage>> DoAnalysisOfWebSiteAsync(List<Uri> urls)
        {
            var analysisWebPages = new ConcurrentBag<AnalysisOfWebPage>();

            _logger.LogInformation("Parallel sending http requests was started.");
            await urls.ParallelForEachAsync(async url =>
            {
                var webPage = await DoAnalysisOfWebPageAsync(url);
                analysisWebPages.Add(webPage);
            }, maxDegreeOfParallelism: 10);
            _logger.LogInformation("Parallel sending http requests was completed.");

            return analysisWebPages.ToList();
        }

        private async Task<AnalysisOfWebPage> DoAnalysisOfWebPageAsync(Uri uri)
        {
            var analysisWebPages = new AnalysisOfWebPage();
            var arrayOfResponseTime = new double[NUBMER_OF_ANALYZES];

            TimeSpan temp;
            for (int i = 0; i < NUBMER_OF_ANALYZES; i++)
            {
                temp = await _requestMonitoring.GetResponseTimeAsync(uri);
                arrayOfResponseTime[i] = temp.TotalMilliseconds;
            }

            _logger.LogInformation(string.Format(
                "Analysis of web page [{0}]: {1}ms", uri, string.Join("ms, ", arrayOfResponseTime)));

            analysisWebPages.Uri = uri.ToString();
            analysisWebPages.MinResponseTime = arrayOfResponseTime.Min();
            analysisWebPages.MaxResponseTime = arrayOfResponseTime.Max();

            return analysisWebPages;
        }
    }
}