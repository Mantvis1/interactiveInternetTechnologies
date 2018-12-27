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
            foreach(var message in messages)
            {
                updatedList.Add(new MessageViewModel(message.ID, message.userId, MDB.GetMessageTypeById(message.messageId), message.date, message.money));
            }
            return updatedList;
        }
    }

    
}