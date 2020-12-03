using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebPageSpeed.Services.WebPageAnalysis;

namespace WebPageSpeed.Controllers
{
    public class MainController : Controller
    {
        private readonly SpeedAnalyzer _speedAnalyzer;

        public MainController(SpeedAnalyzer speedAnalyzer)
        {
            _speedAnalyzer = speedAnalyzer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string uri)
        {
            var webPages = await _speedAnalyzer.DoAnalysisOfWebSiteAsync(uri);
            return View();
        }
    }
}