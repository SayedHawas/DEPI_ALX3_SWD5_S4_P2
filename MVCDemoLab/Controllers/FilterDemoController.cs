using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLab.Controllers
{
    //[Authorize]
    //[HandlerError]
    public class FilterDemoController : Controller
    {
        //[Authorize]
        //[CustomFilter]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        //[HandlerError]
        public IActionResult ShowError()
        {
            throw new Exception();
        }
        public IActionResult ShowErrorTwo()
        {
            throw new Exception();
        }
    }
}
