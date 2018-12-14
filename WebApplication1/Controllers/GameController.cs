using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbContext;

namespace WebApplication1.Controllers
{
    public class GameController : Controller
    {
        GameDB game = new GameDB();
        [HttpGet]
        public ActionResult GameSelection()
        {
            int countOfPlayersInTournament = game.CountOfTournamentPlayers();
            ViewBag.Message = countOfPlayersInTournament;
            return View(countOfPlayersInTournament);
        }
        [HttpGet]
        public ActionResult MyTeam()
        {
            
            return View();
        }

       // [HttpPost]
        public ActionResult RegisterNewCompetotor()
        {
            bool registrationSuccess = game.CreateNewCompetotor((int)Session["id"]);
            if (registrationSuccess == true)
            {
                return RedirectToAction("GameSelection");
            }
            else
            {
                return RedirectToAction("GameSelection"); // Error message
            }
        }
    }
}