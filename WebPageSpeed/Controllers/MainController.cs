using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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

            if (Uri.CheckHostName(domain) == UriHostNameType.Unknown)
            {
                ModelState.AddModelError(string.Empty, "Incorrect input!");
                return View();
            }

            var webSite = new WebSite();

            webSite = await _webSiteAnalyzer.DoAnalysisAsync(domain);

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

        public async Task<IActionResult> History(int page = 1)
        {
            int pageSize = 20;
            var webSites = _webSiteRepository.GetAllOrderedByDateDesc((page - 1) * pageSize, pageSize);
            if (webSites == null)
            {
                return NotFound();
            }

            ViewBag.Page = page;
            ViewBag.ClosedPage = await DetermineClosedNavigationButtonAsync(page, pageSize);
            return View(await webSites.ToListAsync());
        }

        // return 2 if need to close both Previous and Next button
        // return -1 if need to close Previous button
        // return 1 if need to close Next button
        // return 0 if needn't to close
        public async Task<int> DetermineClosedNavigationButtonAsync(int page, int pageSize)
        {
            var count = await _webSiteRepository.GetCountAsync();
            if (count <= pageSize)
            {
                return 2;
            }

            if (page == 1)
            {
                return -1;
            }

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (page == totalPages)
            { 
                return 1;
            }

            return 0;
        }
    }
}