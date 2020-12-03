using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Services.WebSiteAnalysis.Interface;

namespace WebPageSpeed.Controllers
{
    public class MainController : Controller
    {
        private readonly IWebSiteAnalyzer _webSiteAnalyzer;
        private readonly IWebSiteRepository _webSiteRepository;

        public MainController(IWebSiteAnalyzer webSiteAnalyzer, IWebSiteRepository webSiteRepository)
        {
            _webSiteAnalyzer = webSiteAnalyzer;
            _webSiteRepository = webSiteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string uri)
        {
            var webSite = await _webSiteAnalyzer.DoAnalysisAsync(uri);
            return RedirectToAction(nameof(Result), new { websiteId = webSite.Id });
        }

        public async Task<ActionResult> Result(long websiteId)
        {
            var webSite = await _webSiteRepository.GetByIdWithWebPagesAsync(websiteId);
            if (webSite == null)
            {
                return NotFound();
            }
            return View(webSite);
        }

        public async Task<IActionResult> History()
        {
            var webSites = await _webSiteRepository.GetAllOrderedByDateDesc().ToListAsync();
            if (webSites == null)
            {
                return NotFound();
            }
            return View(webSites);
        }
    }
}