using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDemoLab.Data;
using MVCDemoLab.Models;

namespace MVCDemoLab.Controllers
{
    /*
     * 
     * CURD  Create - Update - Read - Read one - Delete 
     * resful
     Routing Mapp

     MVC 
     URL --> http://www.localhost:90/Cateories/List
                                     Controllers / Action /ID?  
     */

    public class CategoriesController : Controller
    {
        private readonly MVCDbContext _context;
        public CategoriesController(MVCDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //Pass Data between Controller To View 
            //Model
            var result = _context.Categories.ToList();
            return View(result);
        }

        //Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(newCategory);
            }
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cate = _context.Categories.Find(id);
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Category updateCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategory);
            }
            _context.Entry(updateCategory).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var cate = _context.Categories.Find(id);
            return View(cate);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cate = _context.Categories.Find(id);
            return View(cate);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ComfirmDelete(int id)
        {
            var cate = _context.Categories.Find(id);
            _context.Categories.Remove(cate);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
