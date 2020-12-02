using System.Collections.Generic;
using WebPageSpeed.Services.Sitemap.Interfaces;

namespace WebPageSpeed.Services.Sitemap
{
    public class SitemapDeterminatorMock : ISitemapDeterminator
    {
        public List<string> GetListOfUrls(string uri)
        {
            return new List<string> 
            {
                "https://www.microsoft.com/uk-ua/",
                "https://templates.office.com/",
                "https://www.google.com/"
            };
        }
    }
}