using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlayersController : Controller
    {
        PlayerManagerController PC = new PlayerManagerController();
        UserDB DB = new UserDB();
        PlayerDB PDB = new PlayerDB();

        public ActionResult Market()
        {
            ViewBag.Message = "Players buy/sell page";
            List<PlayerViewModel> players = PC.getUpdatedListOfPlayers();
            players = players.OrderByDescending(x => x.Eff).ToList();
            return View(players);
        }

        private ActionResult AddPlayer(int? playerId)
        {
            if (playerId != null && Session["id"] != null)
            {
                int id = Convert.ToInt32(playerId);
                DB.insertPlayerToUser((int)Session["id"], id);
            }
            return RedirectToAction("Market");
        }

        public ActionResult MyTeam()
        {
            List<int> playerId = DB.getUserPlayerIdList((int)Session["id"]);
            List<PlayerModel> players = new List<PlayerModel>();
            foreach(int id in playerId)
            {
                players.Add(new PlayerModel(id, PDB.getPlayerById(id)));
            }
            return View(players);
        }
    }
}