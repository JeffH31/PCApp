using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCApp.Models;
using System.Data.Entity;

namespace PCApp.Controllers
{
    public class HomeController : Controller
    {
        private PCDBContext db = new PCDBContext();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

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

            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    db.Users.Add(new User
                    {
                        UserName = username,
                        Password = password
                    });
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        //write e
                    }
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
                    TempData["ID"] = user.UserID;
                    return RedirectToAction("UserProfile");
                }
                else
                {
                    //credentials match not found
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            int ID = Convert.ToInt32(TempData["ID"]);
            if (ID > 0)
            {
                User user = db.Users
                        .Where(u => u.UserID == ID)
                        .Include(u => u.Decks)
                        .SingleOrDefault();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult DeckDetails(int ID)
        {
            IQueryable<Assignment> assignments = db.Assignments
                .Where(a => a.DeckID == ID)
                .Include(a => a.Card)
                .Include(a => a.Deck);                

            return View(assignments);
        }

        [HttpGet]
        public void DeleteDeck(int ID)
        {
            var deck = db.Decks.FirstOrDefault(d => d.DeckID == ID);

            IQueryable<Assignment> assignments = db.Assignments
                .Where(a => a.DeckID == ID);

            if (assignments.Count() > 0 && deck != null)
            {
                foreach (Assignment assignment in assignments)
                {
                    db.Assignments.Remove(assignment);
                }
                db.Decks.Remove(deck);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    //write e
                }
                return View("Login");//eventually should take them back to the UserProfile page
            }
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