using System;

namespace WebApplication1.Models
{
    public class PlayerViewModel :BaseModel
    {
        public string Name { get; set; }
        public double Points { get; set; }
        public double Eff { get; set; }

        public PlayerViewModel(int id, string name, double points, double eff) : base(id)
        {
            Name = name;
            Points = points;
            Eff = eff;
        }

        public double getCost()
        {
            return Math.Round(Eff * 500, 0);
        }

    }
}