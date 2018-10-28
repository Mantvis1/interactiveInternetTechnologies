using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PlayerViewModel : PlayerModel
    {
        public int Cost { get; set; }

        public PlayerViewModel(int cost,string name, string surname, int points, double eff) : base(name, surname, points, eff)
        {
            Cost = cost;
            Name = name;
            Surname = surname;
            Points = points;
        }
    }
}