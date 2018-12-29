using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeamCostController : Controller
    {
        UserDB UDB = new UserDB();
        PlayerDB PDB = new PlayerDB();

       public TeamCostController()
        {

        }

        public void GetTeamCost(int id)
        {
            double cost = 0;
            List<int> player = UDB.getUserPlayerIdList(id);
            foreach (var item in player)
            {
                PlayerViewModel playerEff = new PlayerViewModel(item, "",0, PDB.getEffById(item));
                cost += playerEff.getCost(0);
            }

            UDB.updateUserTeamValue(id, cost);
        }
      
    }
}