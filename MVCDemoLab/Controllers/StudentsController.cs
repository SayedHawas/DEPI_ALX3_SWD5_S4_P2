using Microsoft.AspNetCore.Mvc;
using MVCDemoLab.Models;
namespace MVCDemoLab.Controllers
{
    public class StudentsController : Controller
    {

        public IActionResult Index()
        {
            var result = Student.Students.ToList();
            return View(result);
        }
        //students/showmsgwithpara/100
        public IActionResult ShowMsgWithPara(int id)
        {
            ContentResult result = new ContentResult();
            result.Content = $"Welcome Message With Id {id}";
            return result;
        }
        //students/showmsgwithpara/100
        public IActionResult ShowMsgWithname(string name)
        {
            ContentResult result = new ContentResult();
            result.Content = $"Welcome Message With Name {name}";
            return result;
        }

        public IActionResult BacktoIndex()
        {
            return RedirectToAction("Index");
        }

        public IActionResult BacktoIndexHome()
        {
            //......
            return RedirectToAction("Index", controllerName: "Home");
        }

        public IActionResult ShowGoogle()
        {
            return Redirect("http://www.Google.com");
        }

        public IActionResult ShowYahoo()
        {
            return RedirectPermanent("http://www.Yahoo.com");
        }
        //students/sayhello
        public string SayHello()
        {
            return "Hello World from MVC ....";
        }

        //1- Content "String"   => ContentResult
        public IActionResult SayContent()
        {
            ContentResult result = new ContentResult();
            result.Content = "Hello World from MVC ....";
            return result;
        }

        //2- View "HTML"        => ViewResult
        public IActionResult ShowView()
        {
            ViewResult result = new ViewResult();
            result.ViewName = "~/Views/Students/MyView.cshtml";
            return result;
        }

        //3- Json               => JsonResult
        public IActionResult ShowJson()
        {
            return Json(new { ID = 1, Name = "Osama", Salary = 20000 });
        }

        //4- File               => FileResult
        public IActionResult ShowFile()
        {
            return File("~/Smart Welcome in MVC.txt", "text/plain");
        }
        //5- Empty              => Emptyresult
        public IActionResult ShowEmpty()
        {
            return new EmptyResult(); //or return null;
        }
    }
}
