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

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View("CreateUser");
        }

        [HttpPost]
        public ActionResult CreateUser(string username, string password)
        {
            var user = db.Users.FirstOrDefault(usr => usr.UserName == username);

            if(ModelState.IsValid)
            {
                if (user == null)
                {
                    db.Users.Add(new User
                    {
                        UserName = username,
                        Password = password
                    });
                    db.SaveChanges();
                }
                else
                {
                    //this username already exists
                }
            }
            return View("Index");//User Profile
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(usr => usr.Password == Password && usr.UserName == UserName);
                if (user is null)
                {
                    //Do an alert
                }
                if (user.UserName == UserName && user.Password == Password)
                {
                    //success! Move to account screen
                    return RedirectToAction("UserProfile", "Home", user.UserID);
                }
                else
                {
                    //credentials match not found
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult UserProfile(int userID)
        {
            User user = db.Users.FirstOrDefault(usr => usr.UserID == userID);
            return View(user);
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