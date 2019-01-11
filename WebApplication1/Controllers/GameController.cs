using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GameController : Controller
    {
        private GameDB game = new GameDB();
        private RankingDB ranking = new RankingDB();
        private UserDB uDB = new UserDB();
        private MessageDB mDB = new MessageDB();
        private int maxNumberOfUsers = 8;

        [HttpGet]
        public ActionResult GameSelection()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("About", "Home");
            }
            int countOfPlayerInTournament = game.CountOfTournamentPlayer();
            if (Session["error"] != null)
            {
                ViewBag.Error = Session["error"];
                Session["error"] = null;
            }
            if (countOfPlayerInTournament == maxNumberOfUsers)
            {
                TournamentManageController tournament = new TournamentManageController();
                ViewBag.Error = "Turnyras vyksta. Apie rezultatus bus pranesta zinute";
            }
            ViewBag.Message = countOfPlayerInTournament;
            var viewTournamentModel = new ViewTournamentModel
            {
                NumberOfUsers = countOfPlayerInTournament,
                PartOfRequiredUsers = countOfPlayerInTournament * 100 / maxNumberOfUsers
            };
            return View(viewTournamentModel);
        }

        // [HttpPost]
        public ActionResult RegisterNewCompetotor()
        {
            bool isUserHavePlayers = uDB.isUserHaveAtLeastOnePlayer((int)Session["id"]);
            if (isUserHavePlayers == true)
            {
                game.CreateNewCompetotor((int)Session["id"]);
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
                mDB.deleteMessageById(Convert.ToInt32(messageId));
            }
            return RedirectToAction("Messages");
        }

        [HttpPost]
        public ActionResult DeleteAllMessages()
        {
            mDB.deleteAllMessages((int)Session["id"]);
            return RedirectToAction("Messages");
        }

        private List<MessageViewModel> getUpdatedListOfMessages()
        {
            List<MessageModel> messages = mDB.getAllUserMessages((int)Session["id"]);
            List<MessageViewModel> updatedList = new List<MessageViewModel>();
            foreach (var message in messages)
            {
                updatedList.Add(new MessageViewModel(message.ID, message.UserId, mDB.GetMessageTypeById(message.MessageId), message.Date, message.Money));
            }
            return updatedList;
        }
    }


}