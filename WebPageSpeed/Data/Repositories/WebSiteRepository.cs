using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<WebSite> GetByIdWithWebPagesAsync(long id)
        {
            return await GetAll()
                .Include(w => w.WebPages)
                .SingleOrDefaultAsync(w => w.Id == id);
        }

        public IQueryable<WebSite> GetAllOrderedByDateDesc()
        {
            return GetAll().OrderByDescending(w => w.DateOfAnalysis);
        }
    }
}