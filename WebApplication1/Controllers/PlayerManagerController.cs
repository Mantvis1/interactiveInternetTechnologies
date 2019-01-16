using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlayerManagerController : Controller
    {
        public PlayerManagerController()
        {

        }

        public int playerEff(int pts, int reb, int ast, int stl, int blk, int missedFG, int missedFT, int to)
        {
            return pts + reb + ast + blk - missedFG - missedFT - to;
        }
    }
}


