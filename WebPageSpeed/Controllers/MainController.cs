using Microsoft.AspNetCore.Mvc;

namespace WebPageSpeed.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string uri)
        {
            return View();
        }
    }
}