using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCDemoLab.Controllers
{
    public class FullCategoriesController : Controller
    {
        //private readonly MVCDbContext _context;

        //public FullCategoriesController(MVCDbContext context)
        //{
        //    _context = context;
        //}
        private readonly IServiceCategory _service;
        public FullCategoriesController(IServiceCategory service)
        {
            this._service = service;
        }
        // GET: FullCategories
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Categories.ToListAsync());
            var result = await _service.GetCategoriesAsync();
            return View(result);
        }
        // GET: FullCategories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategotyId == id);
            var category = await _service.GetCategoryByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: FullCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FullCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(category);
                //await _context.SaveChangesAsync();
                _service.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: FullCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var category = await _context.Categories.FindAsync(id);
            var category = await _service.GetCategoryByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: FullCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategotyId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(category);
                    //await _context.SaveChangesAsync();
                    _service.UpdateCategory(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CategoryExists(category.CategotyId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: FullCategories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategotyId == id);
            var category = await _service.GetCategoryByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: FullCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var category = await _context.Categories.FindAsync(id);
            var category = await _service.GetCategoryByIDAsync(id);
            if (category != null)
            {
                _service.DeleteCategory(id);
                //_context.Categories.Remove(category);
            }
            else
            {
                return NotFound();
            }
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool CategoryExists(int id)
        //{
        //    return _context.Categories.Any(e => e.CategotyId == id);
        //}
    }
}
