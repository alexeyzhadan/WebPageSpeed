using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebPageSpeed.Services.Sitemap.Interfaces
{
    public interface ISitemapDeterminator
    {
        Task<List<Uri>> GetListOfUrlsAsync(string uri);
    }
}