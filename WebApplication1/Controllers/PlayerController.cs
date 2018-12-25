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
        private int numberInPage = 10;
        List<PlayerViewModel> localPlayers = new List<PlayerViewModel>();

        [HttpGet]
        public ActionResult Market()
        {
            List<PlayerViewModel> localPlayers = PC.getUpdatedListOfPlayers();
            List<PlayerViewModel> showPlayers = new List<PlayerViewModel>();
            double pageCount = localPlayers.Count / numberInPage;
            PagingModel page = new PagingModel();
            if (Session["currentPage"] == null)
            {
                showPlayers.AddRange(localPlayers.GetRange(0, numberInPage));
                page = new PagingModel(current: 1, last: (Convert.ToInt32(Math.Floor(pageCount) + 1)));
            }
            else
            {
                int startIndex = ((int)Session["currentPage"] - 1) * numberInPage;
                if (localPlayers.Count - startIndex < numberInPage)
                {
                    showPlayers.AddRange(localPlayers.GetRange(((int)Session["currentPage"] - 1) * numberInPage, localPlayers.Count - startIndex));
                }
                else
                {
                    showPlayers.AddRange(localPlayers.GetRange(((int)Session["currentPage"] - 1) * numberInPage, numberInPage));
                }
                page = new PagingModel(current: (int)Session["currentPage"], last: (Convert.ToInt32(Math.Floor(pageCount) + 1)));
            }
            Session["currentPage"] = null;
            var MarketViewModel = new PagedViewModel
            {
                players = showPlayers,
                Page = page
            };
            return View(MarketViewModel);
        }

        [HttpGet]
        public ActionResult MyTeam()
        {
            List<int> playerId = DB.getUserPlayerIdList((int)Session["id"]);
            List<PlayerViewModel> players = new List<PlayerViewModel>();
            foreach (int id in playerId)
            {
                players.Add(new PlayerViewModel(id, PDB.getPlayerById(id), PDB.getPlayerPointsById(id), 0));
            }
            players.OrderBy(x => x.Points);
            return View(players);
        }

        // jokiu budu nedet private, 3 valandas aiskinausi kodel neranda o čia todėl kad įdėjau "private ActionResult BuyPlayer()"
        [HttpPost]
        public ActionResult BuyPlayer(int playerId)
        {
            int foundPlayerCount = DB.isUserHavePlayerById(playerId);
            if (foundPlayerCount == -1)
            {
                //Session error
            }
            else if (foundPlayerCount == 0)
            {
                DB.insertPlayerToUser((int)Session["id"], playerId);
            }
            else
            {
                // Toks zaidejas jau egzistuoja/ Prikti negalima
            }
            return RedirectToAction("Market");
        }

        [HttpPost]
        public ActionResult SellPlayer(int playerId)
        {
            bool isDeleted = DB.DeletePlayerById(playerId, (int)Session["id"]);
            if (isDeleted == true)
            {

            }
            else
            {
                // klaida
            }
            return RedirectToAction("MyTeam");
        }

        //   [HttpPost]
        public ActionResult ChangePage(int? pageNumber)
        {
            if (pageNumber != null)
            {
                Session["currentPage"] = Convert.ToInt32(pageNumber);
            }
            return RedirectToAction("Market");
        }
    }
}