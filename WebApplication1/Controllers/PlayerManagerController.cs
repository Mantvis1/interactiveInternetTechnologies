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
        //List<PlayerModel> players = PDB.getAllPlayers();

        public PlayerManagerController()
        {

        }

        public int playerEff(int pts, int reb, int ast, int stl, int blk, int missedFG, int missedFT, int to)
        {
            return pts + reb + ast + blk - missedFG - missedFT - to;
        }
    }
}


