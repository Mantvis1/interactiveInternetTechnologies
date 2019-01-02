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
        MessageDB MDB = new MessageDB();


        [HttpGet]
        public ActionResult GameSelection()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("About", "Home");
            }
            int countOfPlayerInTournament = game.CountOfTournamentPlayer();
            if(Session["error"] != null)
            {
                ViewBag.Error = Session["error"];
                Session["error"] = null;
            }
            if (countOfPlayerInTournament == 8)
            {
                TournamentManageController tournament = new TournamentManageController();
                ViewBag.Error = "Turnyras vyksta. Apie rezultatus bus pranesta zinute";
            }
            ViewBag.Message = countOfPlayerInTournament;
            return View(countOfPlayerInTournament);
        }

        // [HttpPost]
        public ActionResult RegisterNewCompetotor()
        {
            bool isUserHavePlayers = DB.isUserHaveAtLeastOnePlayer((int)Session["id"]);
            if (isUserHavePlayers == true)
            {
                bool registrationSuccess = game.CreateNewCompetotor((int)Session["id"]);
            }
            else
            {
                Session["error"] = "Prieš registruojantis į turnyrą reikia turėti bent 1 žaidėją!";
            }
            return RedirectToAction("GameSelection");
        }

        public ActionResult Ranking()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("About", "Home");
            }
            List<RankingModel> listOfRanks = ranking.getAllRankings();
            return View(listOfRanks);
        }


        [HttpGet]
        public ActionResult Messages()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            List<MessageViewModel> messages = getUpdatedListOfMessages();
            return View(messages);
        }

        [HttpPost]
        public ActionResult DeleteSelectedMessage(int? messageId)
        {
            if (messageId != null)
            {
                MDB.deleteMessageById(Convert.ToInt32(messageId));
            }
            return RedirectToAction("Messages");
        }

        [HttpPost]
        public ActionResult DeleteAllMessages()
        {
            MDB.deleteAllMessages((int)Session["id"]);
            return RedirectToAction("Messages");
        }

        private List<MessageViewModel> getUpdatedListOfMessages()
        {
            List<MessageModel> messages = MDB.getAllUserMessages((int)Session["id"]);
            List<MessageViewModel> updatedList = new List<MessageViewModel>();
            foreach (var message in messages)
            {
                updatedList.Add(new MessageViewModel(message.ID, message.userId, MDB.GetMessageTypeById(message.messageId), message.date, message.money));
            }
            return updatedList;
        }
    }


}