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
    }
}
