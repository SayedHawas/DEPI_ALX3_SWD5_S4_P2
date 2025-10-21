using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLab.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
