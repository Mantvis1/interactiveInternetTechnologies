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

        private List<int> getListOfCosts()
        {
            List<int> costs = new List<int>();
            /*   for (int i = 0; i < players.Count; i++)
               {

                   costs.Add(Convert.ToInt32(Effectivity(players[i]) * 500));
               }*/
            return costs;
        }

        private double Effectivity(PlayerModel P)
        {
            double sum = 1;
            int count = 1;
            /*for (int i = 0; i < players.Count; i++)
            {
                if (P.Name == players[i].Name && P.Surname == players[i].Surname)
                {
                    sum += players[i].Eff;
                    count++;
                }
            }*/
            return sum / count;
        }
        public void getUpdatedListOfPlayers()
        {
           /* List<int> costs = getListOfCosts();
            List<PlayerViewModel> updatedListOfPlayers = new List<PlayerViewModel>();
            for (int i = 0; i < costs.Count; i++)
            {
                updatedListOfPlayers.Add(new PlayerViewModel(costs[i], players[i].Name, players[i].Surname, players[i].Points, players[i].Eff));
            }
            var result = new List<PlayerViewModel>();
            for (int i = 0; i < updatedListOfPlayers.Count; i++)
            {
                addNewPlayerIfItDoesntExist(updatedListOfPlayers[i], result);
            }
            return result;*/
        }
        private void addNewPlayerIfItDoesntExist(PlayerViewModel playerViewModel, List<PlayerViewModel> result)
        {
            bool isFound = false;
            for (int i = 0; i < result.Count; i++)
            {
                if (playerViewModel.Name == result[i].Name && playerViewModel.Surname == result[i].Surname)
                {
                    isFound = true;
                }
            }
            if (isFound == false)
            {
                result.Add(playerViewModel);
            }
        }
    }
}


