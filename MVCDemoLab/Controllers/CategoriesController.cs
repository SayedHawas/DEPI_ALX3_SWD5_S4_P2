using Microsoft.AspNetCore.Mvc;
using MVCDemoLab.Data;
using MVCDemoLab.Models;

namespace MVCDemoLab.Controllers
{
    public class CategoriesController : Controller
    {
        //EF Core -- Database View CRUD

        private readonly MVCDbContext _dbContext;
        public CategoriesController(MVCDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ActionResult Index()
        {
            //Pass Value From Controller To View Model 
            var list = _dbContext.Categories.ToList();

            //
            ViewData["ApplicationName"] = "Welcome in View Data"; //Object
            ViewData.Add("N1", 150);
            ViewData.Add("N2", 150);

            ViewBag.Number1 = 120;
            ViewBag.Number2 = 885;
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(newCategory);
            }
            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var cate = _dbContext.Categories.Find(id);
            return View(cate);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cate = _dbContext.Categories.Find(id);
            return View(cate);
        }
        [HttpPost]
        public ActionResult Edit(Category updateCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategory);
            }
            _dbContext.Entry(updateCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var cate = _dbContext.Categories.Find(id);
            return View(cate);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDetete(int id)
        {
            var cate = _dbContext.Categories.Find(id);
            _dbContext.Categories.Remove(cate);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
//@htmlHelper
#region OLD Code 

/*
  * 
  * CURD  Create - Update - Read - Read one - Delete 
  * resful
  Routing Mapp

  MVC 
  URL --> http://www.localhost:90/Cateories/List
                                  Controllers / Action /ID?  
  */

//private readonly MVCDbContext _context;
//public CategoriesController(MVCDbContext context)
//{
//    this._context = context;
//}
//[HttpGet]
//public IActionResult Index()
//{
//    //Pass Data between Controller To View 
//    //Model
//    var result = _context.Categories.ToList();
//    return View(result);
//}

////Create 
//[HttpGet]
//public IActionResult Create()
//{
//    return View();
//}

//[HttpPost]
//public IActionResult Create(Category newCategory)
//{
//    if (!ModelState.IsValid)
//    {
//        return View(newCategory);
//    }
//    _context.Categories.Add(newCategory);
//    _context.SaveChanges();
//    return RedirectToAction("Index");
//}

////Edit 
//[HttpGet]
//public IActionResult Edit(int id)
//{
//    var cate = _context.Categories.Find(id);
//    return View(cate);
//}

//[HttpPost]
//public IActionResult Edit(Category updateCategory)
//{
//    if (!ModelState.IsValid)
//    {
//        return View(updateCategory);
//    }
//    _context.Entry(updateCategory).State = EntityState.Modified;
//    _context.SaveChanges();
//    return RedirectToAction("Index");
//}


//[HttpGet]
//public IActionResult Details(int id)
//{
//    var cate = _context.Categories.Find(id);
//    return View(cate);
//}

//[HttpGet]
//public IActionResult Delete(int id)
//{
//    var cate = _context.Categories.Find(id);
//    return View(cate);
//}

//[HttpPost]
//[ActionName("Delete")]
//public IActionResult ComfirmDelete(int id)
//{
//    var cate = _context.Categories.Find(id);
//    _context.Categories.Remove(cate);
//    _context.SaveChanges();
//    return RedirectToAction("Index");
//}
#endregion