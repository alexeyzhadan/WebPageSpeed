using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurnerSoftware.SitemapTools;
using WebPageSpeed.Services.Sitemap.Interfaces;

namespace WebPageSpeed.Services.Sitemap
{
    public class SitemapDeterminator : ISitemapDeterminator
    {
        public async Task<List<Uri>> GetListOfUrlsAsync(string domain)
        {
            var listOfUri = new List<Uri>();
            var sitemapQuery = new SitemapQuery();

            var sitemaps = await sitemapQuery
                .GetAllSitemapsForDomainAsync(domain);

            listOfUri.AddRange(
                sitemaps.SelectMany(
                    s => s.Urls.Select(u => u.Location))
                .Distinct());

            return listOfUri;
        }
    }
}