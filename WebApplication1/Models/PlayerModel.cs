using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PlayerModel : BaseModel
    {
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Points { get; set; }
        public double Eff { get; set; }

        public PlayerModel(string name, string surname, int points, double eff) : base()
        {
            Name = name;
            Surname = surname;
            Points = points;
            Eff = eff;
        }
    }
}