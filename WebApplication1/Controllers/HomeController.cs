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
            //   UpdateExistingDatabase();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        private void UpdateExistingDatabase()
        {
            List<EffModel> eff = new List<EffModel>();
            List<PlayerModel> Player = player.getAllPlayers();
            bool clearedInfoTable = player.clearAllEffTable();
            /*  bool cleared = player.clearAllPlayerTable();
              if(cleared == true)
              {
                  foreach (var onePlayer in Player)
                  {
                     player.addPlayer(onePlayer);
                  }

              }
              */
            foreach (var item in Player)
            {
                eff.Add(player.getEff(item.ID));
            }

            if (clearedInfoTable == true)
            {
                foreach (var onePlayer in eff)
                {
                    player.addPlayerInfo(onePlayer);
                }
            }
        }
    }
}