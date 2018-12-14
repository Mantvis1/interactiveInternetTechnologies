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

        public ActionResult GameSelection()
        {
            int countOfPlayersInTournament = game.CountOfTournamentPlayers();
            ViewBag.Message = countOfPlayersInTournament;
            return View(countOfPlayersInTournament);
        }

        public ActionResult MyTeam()
        {
            
            return View();
        }
    }
}