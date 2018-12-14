using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GameController : Controller
    {
        GameDB game = new GameDB();
        RankingDB ranking = new RankingDB();

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

        public ActionResult Ranking()
        {
            List<RankingModel> listOfRanks = ranking.getAllRankings(1, 2);
            return View(listOfRanks);
        }
    }
}