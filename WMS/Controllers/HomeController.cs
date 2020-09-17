using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Inventory()
        {
            return View();
        }

        public ActionResult Warehouse()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Category()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}