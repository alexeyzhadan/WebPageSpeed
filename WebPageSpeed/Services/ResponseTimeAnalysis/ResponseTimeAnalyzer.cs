using Dasync.Collections;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<List<WebPage>> DoAnalysisOfWebSiteAsync(List<string> links)
        {
            var webPages = new List<WebPage>();

            _logger.LogInformation("Analysis was started!");

            await links.ParallelForEachAsync(async link => 
            {
                var webPage = await DoAnalysisOfWebPageAsync(link);
                webPages.Add(webPage);
            }, maxDegreeOfParallelism: 10);

            _logger.LogInformation("Analysis was completed!");

            return webPages;
        }

        private async Task<WebPage> DoAnalysisOfWebPageAsync(string uri)
        {
            var webPage = new WebPage();
            var arrayOfResponseTime = new double[NUBMER_OF_ANALYZES];

            TimeSpan temp;
            for (int i = 0; i < NUBMER_OF_ANALYZES; i++)
            {
                temp = await _requestMonitoring.GetResponseTimeAsync(uri);
                arrayOfResponseTime[i] = temp.TotalMilliseconds;
            }

            _logger.LogInformation(string.Format(
                "Analysis of web page [{0}]: {1}ms", uri, string.Join("ms, ", arrayOfResponseTime)));

            webPage.Uri = uri;
            webPage.MinResponseTime = arrayOfResponseTime.Min();
            webPage.MaxResponseTime = arrayOfResponseTime.Max();

            return webPage;
        }
    }
}