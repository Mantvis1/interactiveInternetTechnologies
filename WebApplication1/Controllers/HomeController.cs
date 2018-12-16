using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        PlayerDB player = new PlayerDB();
        public ActionResult Index()
        {
            UpdateExistingDatabase();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        private void UpdateExistingDatabase()
        {
            List<PlayerModel> players = player.getAllPlayers();
            bool cleared = player.clearAllPlayerTable();
            if(cleared == true)
            {
                foreach (var onePlayer in players)
                {
                   player.addPlayer(onePlayer);
                }
            }
        }
    }
}