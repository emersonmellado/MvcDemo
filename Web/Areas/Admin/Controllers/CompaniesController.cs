using Web.Context;
using Web.Models.Access;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly MvcDemoContext _db = new MvcDemoContext();

        // GET: Admin/Companies
        public async Task<ActionResult> Index()
        {
            return View(await _db.Companies.ToListAsync());
        }

        // GET: Admin/Companies/Details/5
        public async Task<PartialViewResult> Details(int? id)
        {            
            var company = await _db.Companies.FindAsync(id);
           
            return PartialView(company);
        }

        // GET: Admin/Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Company company)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            _db.Companies.Add(company);
            await _db.SaveChangesAsync();
                

            return View(company);
        }

        // GET: Admin/Companies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = await _db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Admin/Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Company company)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            _db.Entry(company).State = EntityState.Modified;
            await _db.SaveChangesAsync();
                
            return View(company);
        }

        // GET: Admin/Companies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = await _db.Companies.FindAsync(id);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Admin/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var company = await _db.Companies.FindAsync(id);
            _db.Companies.Remove(company);
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
