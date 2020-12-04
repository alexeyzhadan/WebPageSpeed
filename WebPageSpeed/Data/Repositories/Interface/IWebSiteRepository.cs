using System.Linq;
using System.Threading.Tasks;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data.Repositories.Interface
{
    public interface IWebSiteRepository : IIdentifiableEntityRepository<WebSite>
    {
        Task<WebSite> GetByIdWithOrderedWebPagesAsync(long id);
        IQueryable<WebSite> GetAllOrderedByDateDesc();
    }
}