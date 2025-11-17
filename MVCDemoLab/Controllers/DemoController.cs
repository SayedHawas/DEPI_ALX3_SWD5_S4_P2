using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLab.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            TempData["AppName"] = "Smart software";
            return Content("Save Data into Temp Data ");
        }

        public IActionResult GetFirst()
        {
            string name = "Empty Name ";
            if (TempData.ContainsKey("AppName"))
            {
                name = TempData.Peek("AppName").ToString();
            }

            return Content(" get Data " + name + " Please check cookies ...");
        }

        public IActionResult GetKeep()
        {
            string name = "Empty Name ";
            if (TempData.ContainsKey("AppName"))
            {
                name = TempData["AppName"].ToString();
                TempData.Keep(); //For All Key
                //TempData.Keep("AppName"); //On Key
            }
            return Content(" get Data " + name + " Please check cookies ...");
        }

        public IActionResult GetTempDataFirst()
        {
            if (TempData.ContainsKey("AppName"))
            {
                //Normal read 
                return Content($" the Temp data AppName is {TempData["AppName"]}");
            }
            else
            {
                return Content($" the Temp data AppName No Name");
            }
        }

        //Set Cookies
        public IActionResult SetCookies()
        {
            Response.Cookies.Append("AppName", "Smart software");  //Session Cookies 20 Min
            Response.Cookies.Append("Number", "120");

            return Content("Cookies Saving ....");
        }
        //Get Cookies
        public IActionResult GetCookies()
        {
            if (Request.Cookies.ContainsKey("AppName"))
            {
                string appName = Request.Cookies["AppName"];
                int Number = int.Parse(Request.Cookies["Number"]);
                return Content($"Cookies:{appName} & {Number}");
            }

            return Content($"Cookies: Empty ...");
        }
        public IActionResult ShowCookies()
        {
            var cookie = Request.Cookies["AppName"];
            ViewBag.MyCookie = cookie;
            return View();
        }
        public IActionResult SetCookiesPersistent()
        {
            CookieOptions cookieOptions = new CookieOptions();
            //cookieOptions.Expires = DateTimeOffset.Now.AddHours(3);
            cookieOptions.Expires = DateTimeOffset.Now.AddDays(15);
            Response.Cookies.Append("CompanyName", "Smart software", cookieOptions);
            return Content("Cookies Persistent Saving ....");
        }
        public IActionResult RemoveCookiesPersistent()
        {
            CookieOptions cookieOptions = new CookieOptions();
            //cookieOptions.Expires = DateTimeOffset.Now.AddHours(3);
            cookieOptions.Expires = DateTimeOffset.Now.AddDays(-1);
            Response.Cookies.Append("CompanyName", "Smart software", cookieOptions);
            return Content("Cookies Persistent Remove ....");
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("name", "Sayed");
            HttpContext.Session.SetInt32("Counter", 100);
            return Content("Save Session ");
        }
        public IActionResult GetSession()
        {
            string name = HttpContext.Session.GetString("name");
            int? counter = HttpContext.Session.GetInt32("Counter");
            return Content($"Name {name} & Counter {counter}  ");
        }

        public IActionResult ShowSession()
        {
            return View();
        }
        public IActionResult ShowQuery()
        {
            var getName = HttpContext.Request.Query["name"];
            ViewBag.Name = getName;
            return View();
        }

        public ActionResult ShowData()
        {
            //session
            HttpContext.Session.SetString("session1", "welcome in My site Until Brower Closes");
            //send data Between
            TempData["FullRequest"] = "TempData";

            //to view 
            ViewData["ViewDataVal"] = "View Data ";
            ViewBag.viewbagval = "View bag ";

            return RedirectToAction("Show");
        }

        public IActionResult Show()
        {
            return View();
        }

        public ActionResult ShowDataPassController()
        {
            //session
            HttpContext.Session.SetString("session1", "welcome in My site Until Brower Closes");
            //send data Between
            TempData["FullRequest"] = "TempData";

            //to view 
            ViewData["ViewDataVal"] = "View Data ";
            ViewBag.viewbagval = "View bag ";

            return RedirectToAction("ShowAnother");
        }

        public IActionResult ShowAnother()
        {
            return View();
        }

        public IActionResult routingInt(int id, string name)
        {
            return Content($" Welcome Id {id} name {name}");
        }

        [Route("route/{name:alpha}")]
        public IActionResult RouteCustom(string name)
        {
            return Content(name);
        }

    }
}
