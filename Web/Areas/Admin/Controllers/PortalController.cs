using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class PortalController : BaseController
    {
        // GET: Admin/Portal
        public ActionResult Index()
        {
            return View();
        }
    }
}