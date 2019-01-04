using System.Collections.Generic;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class PlayerRepository
    {
        private PlayerManagerController PC = new PlayerManagerController();
        private List<PlayerViewModel> players = new List<PlayerViewModel>();

        public PlayerRepository()
        {
            players = PC.getUpdatedListOfPlayers();
        }

        public List<PlayerViewModel> GetAll()
        {
            return players;
        }
    }
}