using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCApp.Models;
using System.Data.Entity;
using PCApp.Helpers;

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
            IQueryable<Card> cards = db.Cards;
            return View(cards);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View("CreateUser");
        }

        [HttpPost]
        public ActionResult CreateUser(string username, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.UserName == username);

            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    db.Users.Add(new User
                    {
                        UserName = username,
                        Password = password
                    });                    
                    db.SaveChanges();

                    User newUser = db.Users.FirstOrDefault(u => u.UserName == username);
                    TempData["ID"] = newUser.UserID;
                }
                else
                {
                    //this username already exists
                }
            }            
            return RedirectToAction("UserProfile");//User Profile
        }

        [HttpPost]
        public ActionResult CreateDeck(string DeckName, string[] Cards)
        {
            int UserID = (int)(Session["UserID"]);
            int[] cardIDs = new int[10];
            string cardName;
            int newDeckID;

            if (UserID > 0)
            {
                //check to see if DeckName is null
                if (DeckName == "")
                {
                    return Json(new { Result = "NO DECK NAME" },
                        JsonRequestBehavior.AllowGet);
                }

                //Check to see if DeckName is already being used by User
                Deck deck = db.Decks
                    .Where(d => d.DeckName == DeckName)
                    .FirstOrDefault();
                if (deck != null)
                {
                    return Json(new { Result = "ALREADY USING THIS DECK NAME" },
                        JsonRequestBehavior.AllowGet);
                }

                //turn cards Array into List                
                List<string> cards = Cards.ToList();

                //check to see if there are enough Cards
                if (cards.Count() < 10)
                {
                    return Json(new { Result = "DECK MUST HAVE 10 CARDS" },
                        JsonRequestBehavior.AllowGet);
                }

                //change CardNames into CardIDs
                Card card;
                for (int i = 0; i < 10; i++)
                {
                    cardName = cards[i].ToString();
                    card = db.Cards
                        .Where(c => c.CardName == cardName)
                        .FirstOrDefault();
                    cardIDs[i] = card.CardID;
                }

                //Check to see if there are more than 2 phenomenons
                List<Card> PhenomenonCards;
                PhenomenonCards = db.Cards
                    .Where(c => c.CardType == "Phenomenon" 
                    && (cardIDs.Contains(c.CardID))).ToList();

                if (PhenomenonCards.Count > 2)
                {
                    return Json(new { Result = "DECK CAN ONLY HAVE 2 PHENOMENONS" },
                        JsonRequestBehavior.AllowGet);
                }

                //Add Deck
                db.Decks.Add(new Deck
                {
                    DeckName = DeckName,
                    UserID = UserID
                });
                db.SaveChanges();

                //get newly created Deck's ID                
                Deck newDeck = db.Decks
                    .Where(d => d.UserID == UserID)
                    .Where(d => d.DeckName == DeckName)
                    .FirstOrDefault();
                newDeckID = newDeck.DeckID;

                //Assign Cards to Deck
                for (int i = 0; i < 10; i++)
                {
                    db.Assignments.Add(new Assignment
                    {
                        DeckID = newDeckID,
                        CardID = cardIDs[i]
                    });
                    db.SaveChanges();
                }
                //Return Success
                return Json(new { Result = "DECK ADDED", url = Url.Action("UserProfile", "Home")});                
            }
            return Json(new { Result = "PLEASE LOG IN/CREATE A PROFILE" },
                    JsonRequestBehavior.AllowGet);
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
                User user = db.Users.FirstOrDefault(usr => usr.Password == Password && usr.UserName == UserName);
                if (user is null)
                {
                    //Do an alert
                }
                if (user.UserName == UserName && user.Password == Password)
                {
                    //success! Move to account screen
                    Session["UserID"] = user.UserID;
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
            int UserID = (int)(Session["userID"]);
            if (UserID > 0)
            {
                User user = db.Users
                        .Where(u => u.UserID == UserID)
                        .Include(u => u.Decks)
                        .FirstOrDefault();
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

                db.SaveChanges();               
            }
        }        

        [HttpGet]
        public ActionResult PlayDeck(int ID)
        {
            IQueryable<Assignment> assignments = db.Assignments
            .Where(a => a.DeckID == ID)
            .Include(a => a.Card)
            .Include(a => a.Deck);

            assignments.ToList();

            return View(assignments);
        }

        [HttpGet]
        public ActionResult NewDeck()
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