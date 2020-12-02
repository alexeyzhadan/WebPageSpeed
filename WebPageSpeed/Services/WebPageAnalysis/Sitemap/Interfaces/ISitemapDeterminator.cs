using System.Collections.Generic;

namespace WebPageSpeed.Services.WebPageAnalysis.Sitemap.Interfaces
{
    public interface ISitemapDeterminator
    {
        List<string> GetListOfUrls(string uri);
    }
}