using System.Threading.Tasks;
using WebPageSpeed.Models;

namespace WebPageSpeed.Services.WebSiteAnalysis.Interface
{
    public interface IWebSiteAnalyzer
    {
        Task<WebSite> DoAnalysisAsync(string domain);
    }
}