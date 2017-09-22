using Web.Context;
using Web.Models.Access;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class ModulesController : BaseController
    {
        private readonly MvcDemoContext _db = new MvcDemoContext();

        // GET: Admin/Modules
        public async Task<ActionResult> Index()
        {
            return View(await _db.Modules.ToListAsync());
        }

        // GET: Admin/Modules/Details/5
        public async Task<PartialViewResult> Details(long? id)
        {            
            var module = await _db.Modules.FindAsync(id);
           
            return PartialView(module);
        }

        // GET: Admin/Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Modules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ModuleId,Code,Name,Icon,Order,Active")] Module module)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            _db.Modules.Add(module);
            await _db.SaveChangesAsync();
                

            return View(module);
        }

        // GET: Admin/Modules/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = await _db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Admin/Modules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ModuleId,Code,Name,Icon,Order,Active")] Module module)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            _db.Entry(module).State = EntityState.Modified;
            await _db.SaveChangesAsync();
                
            return View(module);
        }

        // GET: Admin/Modules/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var module = await _db.Modules.FindAsync(id);

            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Admin/Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            var module = await _db.Modules.FindAsync(id);
            _db.Modules.Remove(module);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
