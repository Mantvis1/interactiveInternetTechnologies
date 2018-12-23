using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlayerController : Controller
    {
        PlayerManagerController PC = new PlayerManagerController();
        UserDB DB = new UserDB();
        PlayerDB PDB = new PlayerDB();

        [HttpGet]
        public ActionResult Market()
        {
            // ViewBag.Message = "Players buy/sell page";
            List<PlayerViewModel> players = PC.getUpdatedListOfPlayers();
            players = players.OrderByDescending(x => x.Eff).ToList();
            return View(players);
        }

        [HttpGet]
        public ActionResult MyTeam()
        {
            List<int> playerId = DB.getUserPlayerIdList((int)Session["id"]);
            List<PlayerViewModel> players = new List<PlayerViewModel>();
            foreach (int id in playerId)
            {
                players.Add(new PlayerViewModel(id, PDB.getPlayerById(id),PDB.getPlayerPointsById(id),0));
            }
            return View(players);
        }

        // jokiu budu nedet private, 3 valandas aiskinausi kodel neranda o čia todėl kad įdėjau "private ActionResult BuyPlayer()"
        [HttpPost]
        public ActionResult BuyPlayer(int playerId)
        {
            int id = Convert.ToInt32(playerId);
            DB.insertPlayerToUser((int)Session["id"], id);
            return RedirectToAction("Market", "Player");
        }
    }
}