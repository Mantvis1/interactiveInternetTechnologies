using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlayerManagerController : Controller
    {
        static PlayerDB PDB = new PlayerDB();


        public PlayerManagerController()
        {

        }


        public int playerEff(int pts, int reb, int ast, int stl, int blk, int missedFG, int missedFT, int to)
        {
            return pts + reb + ast + blk - missedFG - missedFT - to;
        }

        [HttpGet]
        public List<PlayerViewModel> getUpdatedListOfPlayers()
        {
            List<PlayerViewModel> players = new List<PlayerViewModel>();
            List<PlayerModel> playersList = PDB.getAllLocalPlayers();
            for (int i = 0; i < playersList.Count; i++)
            {
                EffModel ef = PDB.getPointsAndEff(playersList[i].ID);
                players.Add(new PlayerViewModel(playersList[i].ID, playersList[i].Name, ef.pts, ef.eff));
            }
            return players;
        }
    }
}


