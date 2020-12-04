using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data.Repositories
{
    public class WebSiteRepository : IdentifiableEntityRepository<WebSite>, IWebSiteRepository
    {
        private const double TWO = 2.0;

        public WebSiteRepository(WebPageSpeedContext context)
            : base(context)
        {
        }

        public async Task<WebSite> GetByIdWithOrderedWebPagesAsync(long id)
        {
            var webSite = await GetAll()
                .Include(w => w.WebPages)
                .SingleOrDefaultAsync(w => w.Id == id);
            webSite.WebPages = webSite.WebPages.OrderByDescending(w =>
                (w.MaxResponseTime + w.MinResponseTime) / TWO).ToList();
            return webSite;
        }

        public IQueryable<WebSite> GetAllOrderedByDateDesc()
        {
            return GetAll().OrderByDescending(w => w.DateOfAnalysis);
        }
    }
}