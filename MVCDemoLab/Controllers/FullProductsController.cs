using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVCDemoLab.Controllers
{
    public class FullProductsController : Controller
    {
        [TempData]
        public string MessageAdd { get; set; }
        [TempData]
        public string MessageDelete { get; set; }

        private readonly MVCDbContext _context;

        public FullProductsController(MVCDbContext context)
        {
            _context = context;
        }

        // GET: FullProducts
        public async Task<IActionResult> Index()
        {
            //E:\#      0   DEPI\EDPI 2025 R3\Group 1 ALX3_SWD5_S4 Fr 2-5 Mon 7-10\#7 .Net Core MVC & WebAPI\Day 5\DEPI_ALX3_SWD5_S4_P2\MVCDemoLab\wwwroot\Images\Products\1.jpg
            //Root\wwwroot\images\products\1.jpg

            var mVCDbContext = _context.Products.Include(p => p.Category);
            List<Product> products = new List<Product>();
            foreach (var item in mVCDbContext)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", item.ImagePath)))
                {
                    products.Add(item);
                }
                else
                {
                    item.ImagePath = "";
                    products.Add(item);
                }
            }
            return View(products.ToList());
        }

        // GET: FullProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.ImagePath)))
            {
                product.ImagePath = "";
            }
            return View(product);
        }

        public async Task<IActionResult> Card(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.ImagePath)))
            {
                return View(product);
            }
            else
            {
                product.ImagePath = "";
                return View(product);
            }
        }

        // GET: FullProducts/Create
        public IActionResult Create()
        {
            ViewData["CategotyId"] = new SelectList(_context.Categories, "CategotyId", "Name");
            return View();
        }

        // POST: FullProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ProductId,Name,Price,Description,ImagePath,CategotyId")] Product product)
        public async Task<IActionResult> Create([ModelBinder(typeof(ProductModelBinder))] Product product, IFormFile photo)
        {
            //if (product.CategotyId == 0)
            //{
            //    ModelState.AddModelError("CategotyId", "Must Select Category ...");
            //    ViewData["CategotyId"] = new SelectList(_context.Categories, "CategotyId", "Name");
            //    return View(product);
            //}
            //Link between Photo Image Path

            string _Extenstion = Path.GetExtension(photo.FileName);  //.jpg
            string _fileName = DateTime.Now.ToString("yyMMddhhssfff") + _Extenstion;

            if (photo != null && photo.Length > 0)
            {
                //~
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", _fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                product.ImagePath = _fileName; //photo.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                MessageAdd = $"Product {product.Name} added";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategotyId"] = new SelectList(_context.Categories, "CategotyId", "Name", product.CategotyId);
            return View(product);
        }

        //public async Task<IActionResult> Gallery()
        //{
        //    return View(await _context.Products.ToListAsync());
        //}

        public async Task<IActionResult> Gallery(int page = 1, int pageSize = 4)
        {
            #region As List 
            //var list = _context.Products.ToList();
            //var totalItems = await _context.Products.CountAsync();
            //var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            //list = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //ViewBag.CurrentPage = page;
            //ViewBag.TotalPages = totalPages;
            //ViewBag.PageSize = pageSize;
            //ViewBag.TotalItems = totalItems;
            //return View(list); 
            #endregion

            var productCount = _context.Products.Count();
            var totalCount = productCount;
            var totalPage = (int)Math.Ceiling((double)totalCount / pageSize);

            // IEnumerable<Product>  Vs IQueryable<Product>
            //    In Memory                in SQL 

            //var products = (_context.Products as IQueryable<Product>).Skip((page - 1) * pageSize).Take(pageSize);

            var products = await _context.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPage;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = productCount;
            return View(products);


        }


        // GET: FullProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.ImagePath)))
            {
                product.ImagePath = "";
            }

            ViewData["CategotyId"] = new SelectList(_context.Categories, "CategotyId", "Name", product.CategotyId);
            return View(product);
        }

        // POST: FullProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile photo)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }


            if (photo != null && photo.Length > 0)
            {
                string _Extenstion = Path.GetExtension(photo.FileName);  //.jpg
                string _fileName = DateTime.Now.ToString("yyMMddhhssfff") + _Extenstion;
                //~
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", _fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                product.ImagePath = _fileName; //photo.FileName;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategotyId"] = new SelectList(_context.Categories, "CategotyId", "Name", product.CategotyId);
            return View(product);
        }

        // GET: FullProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.ImagePath)))
            {
                return View(product);
            }
            else
            {
                product.ImagePath = "";
                return View(product);
            }
            //return View(product);
        }

        // POST: FullProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                //MessageDelete = $"Department {product.Name} Delete ...";
                TempData["MessageDelete"] = $"Product {product.Name} Delete ...";
                //TempData.Keep();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        public IActionResult CheckPrice(decimal Price)
        {
            if (Price < 19999)
            {
                return Json(true);
            }
            return Json(false);

        }
    }
}
