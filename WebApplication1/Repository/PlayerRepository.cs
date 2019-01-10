using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class PlayerRepository
    {
        private PlayerManagerController pC = new PlayerManagerController();
        private List<PlayerViewModel> players = new List<PlayerViewModel>();

        public PlayerRepository()
        {
            players = pC.getUpdatedListOfPlayers();
        }

        public List<PlayerViewModel> GetAll()
        {
            return players;
        }
    }
}