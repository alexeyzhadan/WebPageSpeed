using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data.Repositories
{
    public class WebSiteRepository : IdentifiableEntityRepository<WebSite>, IWebSiteRepository
    {
        public WebSiteRepository(WebPageSpeedContext context)
            : base(context)
        {
        }
    }
}