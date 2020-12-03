using System.Collections.Generic;

namespace WebPageSpeed.Services.Sitemap.Interfaces
{
    public interface ISitemapDeterminator
    {
        List<string> GetListOfUrls(string uri);
    }
}