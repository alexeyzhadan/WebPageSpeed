using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(string uri)
        {
            var webPages = _speedAnalyzer.DoAnalysisOfWebSite(uri);
            return View();
        }
    }
}