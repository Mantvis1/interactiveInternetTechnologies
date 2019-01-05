﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class PlayerController : Controller
    {
        UserDB DB = new UserDB();
        PlayerDB PDB = new PlayerDB();
        private int numberInPage = 10;
        MessageDB MDB = new MessageDB();
        TeamCostController teamCost = new TeamCostController();
        PlayerRepository PR = new PlayerRepository();

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
            List<PlayerViewModel> localPlayers = PR.GetAll();
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
            List<int> playerId = DB.getUserPlayerIdList((int)Session["id"]);
            List<PlayerViewModel> players = new List<PlayerViewModel>();
            foreach (int id in playerId)
            {
                players.Add(new PlayerViewModel(id, PDB.getPlayerById(id), PDB.getPlayerPointsById(id), 0));
            }
            return View(players);
        }

        // jokiu budu nedet private, 3 valandas aiskinausi kodel neranda o čia todėl kad įdėjau "private ActionResult BuyPlayer()"
        [HttpPost]
        public ActionResult BuyPlayer(int playerId, int cost)
        {
            int foundPlayerCount = DB.isUserHavePlayerById((int)Session["id"], playerId);
            if (foundPlayerCount == 0)
            {
                int userMoney = DB.getMoneyById((int)Session["id"]);
                if (userMoney >= cost)
                {
                    int moneyLeft = userMoney - cost;
                    DB.insertPlayerToUser((int)Session["id"], playerId);
                    DB.updateUserMoney((int)Session["id"], moneyLeft);
                    Session["money"] = moneyLeft;
                    MDB.addNewMessage((int)Session["id"], 1, -cost);
                    teamCost.GetTeamCost((int)Session["id"]);
                    Session["success"] = "Sėkmingai nusipirkote žaidėją";
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
            bool isDeleted = DB.DeletePlayerById(playerId, (int)Session["id"]);
            if (isDeleted == true)
            {
                PlayerViewModel player = new PlayerViewModel(playerId, "", 0, PDB.getEffById(playerId));
                int money = DB.getMoneyById((int)Session["id"]);
                int cost = (int)Math.Round(player.getCost(1), 0);
                int moneyLeft = money + cost;
                DB.updateUserMoney((int)Session["id"], (Convert.ToInt32(moneyLeft)));
                Session["money"] = moneyLeft;
                MDB.addNewMessage((int)Session["id"], 2, cost);
                teamCost.GetTeamCost((int)Session["id"]);
                Session["success"] = "Sėkmingai pardavėte žaidėją";
            }
            else
            {
                Session["error"] = "Klaida parduodant žaidėją";
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