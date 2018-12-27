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
        UserDB DB = new UserDB();

        [HttpGet]
        public ActionResult GameSelection()
        {
            int countOfPlayerInTournament = game.CountOfTournamentPlayer();
            ViewBag.Message = countOfPlayerInTournament;
            return View(countOfPlayerInTournament);
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
            List<RankingModel> listOfRanks = ranking.getAllRankings(0, 1000);
            return View(listOfRanks);
        }


        [HttpGet]
        public ActionResult Messages()
        {
            return View();
        }

        
    }
}