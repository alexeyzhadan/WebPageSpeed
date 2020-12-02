using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPageSpeed.Models;
using WebPageSpeed.Services.WebPageAnalysis.Monitoring.Interfaces;
using WebPageSpeed.Services.WebPageAnalysis.Sitemap.Interfaces;

namespace WebPageSpeed.Services.WebPageAnalysis
{
    public class SpeedAnalyzer
    {
        private const int NUBMER_OF_ANALYZES = 3;

        private readonly ISitemapDeterminator _sitemapDeterminator;
        private readonly IRequestMonitoring _requestMonitoring;
        private readonly ILogger<SpeedAnalyzer> _logger;

        public SpeedAnalyzer(
            ISitemapDeterminator sitemapDeterminator,
            IRequestMonitoring requestMonitoring,
            ILogger<SpeedAnalyzer> logger)
        {
            _sitemapDeterminator = sitemapDeterminator;
            _requestMonitoring = requestMonitoring;
            _logger = logger;
        }

        public List<WebPage> DoAnalysisOfWebSite(string uri)
        {
            var webPages = new List<WebPage>();

            _logger.LogInformation("Analysis was started!");

            // determine sitemap
            var links = _sitemapDeterminator.GetListOfUrls(uri);

            //analyse each web pages from sitemap
            Parallel.ForEach(links, link =>
            {
                var webPage = DoAnalysisOfWebPage(link);
                webPages.Add(webPage);
            });

            _logger.LogInformation("Analysis was completed!");

            return webPages;
        }

        private WebPage DoAnalysisOfWebPage(string uri)
        {
            var webPage = new WebPage();
            var arrayOfResponseTime = new double[NUBMER_OF_ANALYZES];

            TimeSpan temp;
            for (int i = 0; i < NUBMER_OF_ANALYZES; i++)
            {
                temp = _requestMonitoring.GetResponseTimeAsync(uri).Result;
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