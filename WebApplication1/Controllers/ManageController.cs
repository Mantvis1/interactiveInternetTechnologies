using System.Web.Mvc;
using WebApplication1.DbContext;

namespace WebApplication1.Controllers
{
    public class ManageController : Controller
    {
        private ManageDB mDB = new ManageDB();

        [HttpGet]
        public ActionResult Settings()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            ViewBag.ErrorMessage = Session["error"];
            Session["error"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string repeatNewPassword)
        {
            if (newPassword == repeatNewPassword)
            {
                if (mDB.UpdatePassword((int)Session["id"], newPassword) == true)
                {
                    Session["error"] = "Sėkmingai paskeistas slaptažodis";
                }
                else
                {
                    Session["error"] = "Klaidą pakeičiant slaptažodį";
                }
            }
            else
            {
                Session["error"] = "Skiriasi įvesti slaptazodziai";
            }
            return RedirectToAction("Settings");
        }

        [HttpPost]
        public ActionResult ChangeUsername(string newUserName, string password)
        {
            if (mDB.isUserExists((int)Session["id"], password) == true)
            {
                mDB.UpdateUsername((int)Session["id"], newUserName);
                Session["name"] = newUserName;
                Session["error"] = "Sekmingai paskeistas vartotojo vardas";
            }
            else
            {
                Session["error"] = "Klaida pakeičiant vartotojo vardą";
            }
            return RedirectToAction("Settings");
        }

        [HttpPost]
        public ActionResult ChangeEmail(string oldEmail, string newEmail, string password)
        {
            if (mDB.isUserExists((int)Session["id"], password) == true)
            {
                mDB.UpdateEmail((int)Session["id"], newEmail);
                Session["error"] = "Sėkmingai pakeistas paštas";
            }
            else
            {
                Session["error"] = "Klaida pakeičiant paštą";
            }
            return RedirectToAction("Settings");
        }

        public ActionResult DeleteAccount(string password)
        {
            if (mDB.isUserExists((int)Session["id"], password) == true)
            {
                mDB.DeleteAccount((int)Session["id"]);
                Session.Clear();
                return RedirectToAction("About", "Home");
            }
            else
            {
                Session["error"] = "Nepavyko ištrinti paskyros";
                return RedirectToAction("Settings");
            }
        }
    }
}