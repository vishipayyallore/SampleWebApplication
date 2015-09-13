using System.Web.Mvc;

namespace WebRole1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Cloud Patterns";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ais";

            return View();
        }
    }
}