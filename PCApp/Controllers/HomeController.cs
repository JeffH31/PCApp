using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCApp.Models;

namespace PCApp.Controllers
{
    public class HomeController : Controller
    {
        private PCDBContext db = new PCDBContext();

        public ActionResult CardsIndex()
        {
            return View(db.Cards.ToList());
        }
        public ActionResult Index()
        {
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