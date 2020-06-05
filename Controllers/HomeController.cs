using System;
using System.Web.Mvc;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (DateTime.Now.Hour < 12)
            {
                ViewBag.greet = $"Good morning {User.Identity.Name}, please choose from the following options:";
            }
            else
            {
                ViewBag.greet = $"Good afternoon {User.Identity.Name}, please choose from the following options:";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}