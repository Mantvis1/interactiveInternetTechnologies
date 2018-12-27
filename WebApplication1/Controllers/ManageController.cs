using System.Web.Mvc;
using WebApplication1.DbContext;

namespace WebApplication1.Controllers
{
    public class ManageController : Controller
    {
        ManageDB MDB = new ManageDB();

        [HttpGet]
        public ActionResult Settings()
        {
            ViewBag.ErrorMessage = Session["error"];
            Session["error"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string repeatNewPassword)
        {
            if (newPassword == repeatNewPassword)
            {
                if (MDB.UpdatePassword((int)Session["id"], newPassword) == true)
                {
                    Session["error"] = "Sekmingai paskeistas slaptažodis";
                }
                else
                {
                    Session["error"] = "Klaida pakeičiant slaptažodį";
                }
            }
            else
            {
                Session["error"] = "Skiriasi ivesti slaptazodziai";
            }
            return RedirectToAction("Settings");
        }

        [HttpPost]
        public ActionResult ChangeUsername(string newUserName, string password)
        {
            if (MDB.isUserExists((int)Session["id"], password) == true)
            {
                MDB.UpdateUsername((int)Session["id"], newUserName);
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
            if (MDB.isUserExists((int)Session["id"], password) == true)
            {
                MDB.UpdateEmail((int)Session["id"], newEmail);
                Session["error"] = "Sekmingai paskeistas pastas";
            }
            else
            {
                Session["error"] = "Klaida pakeičiant paštą";
            }
            return RedirectToAction("Settings");
        }

        // [HttpDelete]
        public ActionResult DeleteAccount(string password)
        {
            if (MDB.isUserExists((int)Session["id"], password) == true)
            {
                MDB.DeleteAccount((int)Session["id"]);
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