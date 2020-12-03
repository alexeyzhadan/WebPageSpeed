using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPageSpeed.Models.ViewModel;
using WebPageSpeed.Services.WebSiteAnalysis.Interface;

namespace WebPageSpeed.Controllers
{
    public class MainController : Controller
    {
        private readonly IWebSiteAnalyzer _webSiteAnalyzer;

        public MainController(IWebSiteAnalyzer webSiteAnalyzer)
        {
            _webSiteAnalyzer = webSiteAnalyzer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string uri)
        {
            var webSite = await _webSiteAnalyzer.DoAnalysisAsync(uri);
            return RedirectToAction(
                nameof(Result),
                new { 
                    authority = webSite.Authority,
                    date = webSite.DateOfAnalysis
                });
        }

        public IActionResult Result(string authority, DateTime date)
        {
            var webPagesResults = new List<WebPageResultViewModel>();
            return View(webPagesResults);
        }

        public IActionResult History()
        {
            return View();
        }
    }
}