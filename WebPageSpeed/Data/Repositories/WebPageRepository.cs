using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data.Repositories
{
    public class WebPageRepository : IdentifiableEntityRepository<WebPage>, IWebPageRepository
    {
        public WebPageRepository(WebPageSpeedContext context) 
            : base(context)
        {
        }
    }
}