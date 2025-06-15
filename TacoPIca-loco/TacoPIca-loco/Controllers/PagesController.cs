using Microsoft.AspNetCore.Mvc;

namespace TacoPIca_loco.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        
        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Situation()
        {
            return View();
        }
    }
}
