using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        UserDB user = new UserDB();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrationForm(string userName, string pass, string email)
        {
            user.AddNewUser(new UserModel(userName, pass, email));
            return RedirectToAction("LogIn");
        }

        [HttpPost]
        public ActionResult CheckLogIn(string username, string password)
        {
            //int count = user.CanBeLogedIn(username, password);
            return View("LogIn");
        }
    }
}