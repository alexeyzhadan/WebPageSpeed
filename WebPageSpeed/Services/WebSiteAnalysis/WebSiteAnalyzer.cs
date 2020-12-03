using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Models;
using WebPageSpeed.Services.ResponseTimeAnalysis.Interface;
using WebPageSpeed.Services.Sitemap.Interfaces;
using WebPageSpeed.Services.WebSiteAnalysis.Interface;

namespace WebPageSpeed.Services.WebSiteAnalysis
{
    public class WebSiteAnalyzer : IWebSiteAnalyzer
    {
        private readonly ISitemapDeterminator _sitemapDeterminator;
        private readonly IResponseTimeAnalyzer _responseTimeAnalyzer;
        private readonly IWebPageRepository _webPageRepository;
        private readonly ILogger<WebSiteAnalyzer> _logger;

        public WebSiteAnalyzer(
            ISitemapDeterminator sitemapDeterminator,
            IResponseTimeAnalyzer responseTimeAnalyzer,
            IWebPageRepository webPageRepository,
            ILogger<WebSiteAnalyzer> logger)
        {
            _sitemapDeterminator = sitemapDeterminator;
            _responseTimeAnalyzer = responseTimeAnalyzer;
            _webPageRepository = webPageRepository;
            _logger = logger;
        }

        public async Task<WebSite> DoAnalysisAsync(string uriString)
        {
            _logger.LogInformation("Start to determine a sitemap.");
            var links = _sitemapDeterminator.GetListOfUrls(uriString);
            _logger.LogInformation("The sitemap was determined.");

            _logger.LogInformation("Start to analyse a website.");
            var analysisWebPages = await _responseTimeAnalyzer.DoAnalysisOfWebSiteAsync(links);
            _logger.LogInformation("The website was analysed.");

            var webSite = new WebSite()
            {
                DateOfAnalysis = DateTime.Now,
                Authority = new Uri(uriString).GetLeftPart(UriPartial.Authority)
            };

            var webPages = new List<WebPage>();
            analysisWebPages.ForEach(a => 
                webPages.Add(
                    new WebPage() 
                    {
                        MaxResponseTime = a.MaxResponseTime,
                        MinResponseTime = a.MinResponseTime,
                        Path = a.Uri.Replace(webSite.Authority, string.Empty),
                        WebSite = webSite
                    }));
            await _webPageRepository.AddRangeAsync(webPages.ToArray());
            _logger.LogInformation("Webpages was added to database.");

            return webSite;
        }
    }
}