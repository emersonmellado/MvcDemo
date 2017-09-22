using System.Web.Mvc;

namespace Web.Controllers
{
    public class AdminPortalController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Portal", new { area= "Admin"});
            //return RedirectToAction("action", "controller", new { area = "area" });
        }
    }
}