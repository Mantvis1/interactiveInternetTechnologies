using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        UserDB user = new UserDB();
        GameDB game = new GameDB();
      //  private string message = "";

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.ErrorMessage = Session["error"];
            Session["error"] = null;
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            ViewBag.ErrorMessage = Session["error"];
            Session["error"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult RegistrationForm(string userName, string pass, string email)
        {
            UserModel newUser = new UserModel(userName, pass, email);
            bool isAdded = user.AddNewUser(newUser);
            if (isAdded == true)
            {
                int id = user.getUserId(userName, pass);
                game.CreateRankingForUser(id);
                return RedirectToAction("LogIn");
            }
            else
            {
                Session["error"] = "Vartotojas tokiu vardu jau yra";
               return RedirectToAction("Register");
            }
        }

        [HttpPost]
        public ActionResult CheckLogIn(string userName, string pass)
        {
            int count = user.CanBeLogedIn(userName, pass);
            if (count == 1)
            {
                int id = user.getUserId(userName, pass);
                if (id != 0)
                {
                    Session["id"] = id;
                    Session["name"] = userName;
                    return RedirectToAction("MyTeam", "Player");
                }
            }
            else
            {
                Session["error"] = "Tokio vartotojo nėra.";
                return View("LogIn");
            }
            return View();
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("About", "Home");
        }
    }
}