using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        UserDB user = new UserDB();
        string message = "";

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.ErrorMessage = "ErrorMessage";
            return View(message);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View(message);
        }

        [HttpPost]
        public ActionResult RegistrationForm(string userName, string pass, string email)
        {
            bool isAdded = user.AddNewUser(new UserModel(userName, pass, email));
            if (isAdded == true)
            {
                message = "Sėkmingai sukurtas naujas vartotojas";
                return RedirectToAction("LogIn");
            }
            else
            {
                message = "Vartotojo sukurti nepavyko, bandykite dar kartą!";
                RedirectToAction("Register");
            }
            return RedirectToAction("Register");
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
                    return RedirectToAction("Market", "Players");
                } else
                {
                    return View("LogIn");
                    //Error, id not found
                }
            }
            else
            {
                return View("LogIn");
            }
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("About", "Home");
        }
    }
}