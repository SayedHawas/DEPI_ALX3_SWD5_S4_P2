using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVCDemoLab.Controllers
{
    public class ProductsController : Controller
    {

        private readonly MVCDbContext _dbContext;
        public ProductsController(MVCDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.Include("Category").ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //Model 
            //ViewData
            //ViewBag 
            //var CategoryList = from category in _dbContext.Categories
            //                   select new { category.CategotyId, category.Name };

            var CategoryList2 = _dbContext.Categories.ToList();
            ViewBag.CategotyId = new SelectList(CategoryList2, "CategotyId", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategotyId = new SelectList(_dbContext.Categories.ToList(), "CategotyId", "Name", newProduct.CategotyId);
                return View(newProduct);
            }
            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
