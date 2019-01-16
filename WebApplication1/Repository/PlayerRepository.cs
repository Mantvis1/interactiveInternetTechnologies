using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.DbContext;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Repository
{
    public class PlayerRepository
    {
        private List<PlayerViewModel> players = new List<PlayerViewModel>();
        private static PlayerDB PDB = new PlayerDB();

        public PlayerRepository()
        {
            players = sortList(getUpdatedListOfPlayers());
        }

        public List<PlayerViewModel> GetAll()
        {
            return players;
        }


        private List<PlayerViewModel> getUpdatedListOfPlayers()
        {
            List<PlayerViewModel> players = new List<PlayerViewModel>();
            List<PlayerModel> playersList = PDB.getAllLocalPlayers();
            for (int i = 0; i < playersList.Count; i++)
            {
                EffModel ef = PDB.getPointsAndEff(playersList[i].ID);
                players.Add(new PlayerViewModel(playersList[i].ID, playersList[i].Name, ef.Pts, ef.Eff));
            }
            return players;
        }

        public int getLength()
        {
            return players.Count;
        }

        public List<PlayerViewModel> getPartOfPlayers(int start, int count)
        {
            return players.GetRange(start, count);
        }

        private List<PlayerViewModel> sortList(List<PlayerViewModel> updated)
        {
            List<PlayerViewModel> sortedList = updated.OrderByDescending(x => x.Eff).ToList();
            return sortedList;
        }
    }
}