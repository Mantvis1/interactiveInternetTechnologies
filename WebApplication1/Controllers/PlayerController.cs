using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class PlayerController : Controller
    {
        private UserDB uDB = new UserDB();
        private PlayerDB pDB = new PlayerDB();
        private int numberInPage = 10;
        private MessageDB mDB = new MessageDB();
        private TeamCostController teamCost = new TeamCostController();
        private PlayerRepository pR = new PlayerRepository();

        [HttpGet]
        public ActionResult Market()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            if (Session["error"] != null)
            {
                ViewBag.Error = Session["error"];
                Session["error"] = null;
            }
            else if (Session["success"] != null)
            {
                ViewBag.SuccessMessage = Session["success"];
                Session["success"] = null;
            }

            List<PlayerViewModel> showPlayers = new List<PlayerViewModel>();

            double pageCount = pR.getLength() / numberInPage;
            int lastPage = (Convert.ToInt32(Math.Floor(pageCount) + 1));

            PagingModel page = new PagingModel();
            if (Session["currentPage"] == null)
            {
                showPlayers.AddRange(pR.getPartOfPlayers(0, numberInPage));
                page = new PagingModel(current: 1, last: lastPage);
            }
            else
            {
                if ((int)Session["currentPage"] <= 0 || (int)Session["currentPage"] > lastPage)
                {
                    Session["currentPage"] = lastPage;
                }

                int startIndex = ((int)Session["currentPage"] - 1) * numberInPage;
                if (pR.getLength() - startIndex < numberInPage)
                {
                    showPlayers.AddRange(pR.getPartOfPlayers(((int)Session["currentPage"] - 1) * numberInPage, pR.getLength() - startIndex));
                }
                else
                {
                    showPlayers.AddRange(pR.getPartOfPlayers(((int)Session["currentPage"] - 1) * numberInPage, numberInPage));
                }
                page = new PagingModel(current: (int)Session["currentPage"], last: lastPage);

            }
            var MarketViewModel = new PagedViewModel
            {
                Players = showPlayers,
                Page = page
            };
            return View(MarketViewModel);
        }

        [HttpGet]
        public ActionResult MyTeam()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            Session["currentPage"] = null;
            if (Session["error"] != null)
            {
                ViewBag.Error = Session["error"];
                Session["error"] = null;
            }
            else if (Session["success"] != null)
            {
                ViewBag.SuccessMessage = Session["success"];
                Session["success"] = null;
            }
            List<int> playerId = uDB.getUserPlayerIdList((int)Session["id"]);
            List<PlayerViewModel> players = new List<PlayerViewModel>();
            foreach (int id in playerId)
            {
                players.Add(new PlayerViewModel(id, pDB.getPlayerById(id), pDB.getPlayerPointsById(id), 0));
            }
            return View(players);
        }

        [HttpPost]
        public ActionResult BuyPlayer(int playerId, int cost)
        {
            int foundPlayerCount = uDB.isUserHavePlayerById((int)Session["id"], playerId);
            if (foundPlayerCount == 0)
            {
                int userMoney = uDB.getMoneyById((int)Session["id"]);
                if (userMoney >= cost)
                {
                    int moneyLeft = userMoney - cost;
                    uDB.insertPlayerToUser((int)Session["id"], playerId);
                    uDB.updateUserMoney((int)Session["id"], moneyLeft);
                    Session["money"] = moneyLeft;
                    mDB.addNewMessage((int)Session["id"], 1, -cost);
                    teamCost.GetTeamCost((int)Session["id"]);
                    Session["success"] = "Sėkmingai nusipirkote žaidėją";
                }
                else
                {
                    Session["error"] = "Jums trūksta pinigu!";
                }
            }
            else
            {
                Session["error"] = "Toks zaidejas jau nupirktas. Pirkti negalima.";
            }
            return RedirectToAction("Market");
        }

        [HttpPost]
        public ActionResult SellPlayer(int playerId)
        {
            bool isRegistratedOnTournament = uDB.IsTournamentMember((int)Session["id"]);
            int count = uDB.getUserPlayerIdList((int)Session["id"]).Count;
            if (isRegistratedOnTournament == false || (isRegistratedOnTournament == true && count > 1))
            {
                bool isDeleted = uDB.DeletePlayerById(playerId, (int)Session["id"]);
                if (isDeleted == true)
                {
                    PlayerViewModel player = new PlayerViewModel(playerId, "", 0, pDB.getEffById(playerId));
                    int money = uDB.getMoneyById((int)Session["id"]);
                    int cost = (int)Math.Round(player.getCost(1), 0);
                    int moneyLeft = money + cost;
                    uDB.updateUserMoney((int)Session["id"], (Convert.ToInt32(moneyLeft)));
                    Session["money"] = moneyLeft;
                    mDB.addNewMessage((int)Session["id"], 2, cost);
                    teamCost.GetTeamCost((int)Session["id"]);
                    Session["success"] = "Sėkmingai pardavėte žaidėją";
                }
                else
                {
                    Session["error"] = "Klaida parduodant žaidėją";
                }
            }
            else
            {
                Session["error"] = "Negalite parduoti paskutinio žaidėjo kadangi dalyvaujate turnyre!";
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

        public ActionResult ChangePageUsingInput(int? getCurrentPageNumber)
        {
            if (getCurrentPageNumber != null)
            {
                Session["currentPage"] = Convert.ToInt32(getCurrentPageNumber);
            }
            return RedirectToAction("Market");
        }
    }
}