using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TurnerSoftware.SitemapTools;
using WebPageSpeed.Services.Sitemap.Interfaces;

namespace WebPageSpeed.Services.Sitemap
{
    public class SitemapDeterminator : ISitemapDeterminator
    {
        private readonly ILogger<SitemapDeterminator> _logger;

        public SitemapDeterminator(ILogger<SitemapDeterminator> logger)
        {
            _logger = logger;
        }

        public async Task<List<Uri>> GetListOfUrlsAsync(string domain)
        {
            var listOfUri = new List<Uri>();
            var sitemapQuery = new SitemapQuery();

            IEnumerable<SitemapFile> sitemaps = null;
            try
            {
                sitemaps = await sitemapQuery
                    .GetAllSitemapsForDomainAsync(domain);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (InvalidOperationException)
            {
                _logger.LogInformation($"Unknown type of sitemap.");
            }
            catch (HttpRequestException)
            {
                _logger.LogInformation($"[{domain}] This host is unknown.");
            }
            finally
            {
                if (sitemaps == null)
                {
                    sitemaps = new List<SitemapFile>();
                }
            }

            listOfUri.AddRange(
                sitemaps.SelectMany(
                    s => s.Urls.Select(u => u.Location))
                .Distinct());

            return listOfUri;
        }
    }
}