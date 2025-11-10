using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLab.Controllers
{
    public class TestDemoController : Controller
    {
        public IActionResult Index()
        {
            ViewData["data"] = "Welcome From MVC ";
            return View();
        }

        public double div(int x, int y)
        {
            return x / y;
        }
    }
}
