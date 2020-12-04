using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Models;
using WebPageSpeed.Services.WebSiteAnalysis.Interface;

namespace WebPageSpeed.Controllers
{
    public class MainController : Controller
    {
        private const int ZERO = 0;
        private const double TWO = 2.0;

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
        public async Task<IActionResult> Index(string domain)
        {
            if (domain == null)
            {
                ModelState.AddModelError(string.Empty, "Domain is required field!");
                return View();
            }

            var webSite = new WebSite();

            try
            {
                webSite = await _webSiteAnalyzer.DoAnalysisAsync(domain);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Website not found! Check your entered domain name.");
                return View();
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "Invalid type of sitemap. Analysis is impossible!");
                return View();
            }

            if (webSite.Id == ZERO)
            {
                ModelState.AddModelError(string.Empty, "Sitemap not found!");
                return View();
            }

            return RedirectToAction(nameof(Result), new { websiteId = webSite.Id });
        }

        public async Task<ActionResult> Result(long? websiteId)
        {
            if (websiteId == null)
            {
                return BadRequest();
            }

            var webSite = await _webSiteRepository
                .GetByIdWithWebPagesAsync((long)websiteId);

            if (webSite == null)
            {
                return NotFound();
            }

            webSite.WebPages = webSite.WebPages.OrderByDescending(w =>
                (w.MaxResponseTime + w.MinResponseTime) / TWO).ToList();

            return View(webSite);
        }

        public async Task<IActionResult> History()
        {
            var webSites = _webSiteRepository.GetAllOrderedByDateDesc();
            if (webSites == null)
            {
                return NotFound();
            }

            return View(await webSites.ToListAsync());
        }
    }
}