using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLab.Controllers
{
    public class ReadWriteController : Controller
    {
        // GET: ReadWriteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReadWriteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReadWriteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReadWriteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadWriteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReadWriteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadWriteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReadWriteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
