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
                if (MDB.UpdatePassword() == true)
                {
                    Session["error"] = "Sekmingai paskeistas slaptažodis";
                }
                else
                {
                    Session["error"] = "Klaida pakeičiant slaptažodį";
                }
            }else
            {
                Session["error"] = "Skiriasi ivesti slaptazodziai";
            }
            return RedirectToAction("Settings");
        }

        [HttpPost]
        public ActionResult ChangeUsername(string newUserName, string password)
        {
            if(MDB.UpdateUsername() == true)
            {
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
            if(MDB.UpdateEmail() == true)
            {
                Session["error"] = "Sekmingai paskeistas pastas";
            }
            else
            {
                Session["error"] = "Klaida pakeičiant paštą";
            }
            return RedirectToAction("Settings");
        }

       // [HttpDelete]
        public ActionResult DeleteAccount()
        {
            if (MDB.DeleteAccount() == true)
            {
                Session.Clear();
                return RedirectToAction("About", "Home");
            } else
            {
                Session["error"] = "Nepavyko ištrinti paskyros";
                return RedirectToAction("Settings");
            }
           
        }
    }
}