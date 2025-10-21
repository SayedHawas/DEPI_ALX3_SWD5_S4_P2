using Microsoft.AspNetCore.Mvc;
using MVCDemoLab.Data;
using MVCDemoLab.ViewModels;

namespace MVCDemoLab.Controllers
{
    public class UsersController : Controller
    {
        private readonly MVCDbContext _dbContext;
        public UsersController(MVCDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = _dbContext.Users.
                   FirstOrDefault(u => u.UserName == userLogin.UserName && u.Password == userLogin.Password);
            if (user == null)
            {
                ViewBag.ErrorLogin = "UserName Or Password are invalid ...";
                return View();
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
