using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DbContext;

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
                cost += PDB.getEffById(item) * 500;
            }

            UDB.updateUserTeamValue(id, cost);
        }
      
    }
}