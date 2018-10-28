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
        public ActionResult Market()
        {
            PlayerManagerController PC = new PlayerManagerController();
            ViewBag.Message = "Players buy/sell page";
            List<PlayerViewModel> players = PC.getUpdatedListOfPlayers();
            return View(players);
        }
    }
}