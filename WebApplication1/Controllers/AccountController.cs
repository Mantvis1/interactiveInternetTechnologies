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
        public ActionResult CheckLogIn(string username, string password)
        {
            /*  int count = user.CanBeLogedIn(username, password);
              if (count == 1)
              {
                  return View("LogIn");
              }*/
            return View();
        }
    }
}