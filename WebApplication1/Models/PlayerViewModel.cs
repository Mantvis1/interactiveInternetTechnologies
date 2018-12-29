using System;

namespace WebApplication1.Models
{
    public class PlayerViewModel : BaseModel
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

        public double getCost(int type) // 0 pirkti 1 parduoti
        {
            if (type == 0)
            {
                if (Eff == 0)
                {
                    return 500 / 2;
                }
                else if (Eff < 0)
                {
                    return 500 / (Eff * -1);
                }
                else
                {
                    return Math.Round(Eff * 500, 0);
                }
            }
            else if (type == 1)
            {
                if (Eff == 0)
                {
                    return (500 / 2) * 0.95;
                }
                else if (Eff < 0)
                {
                    return (500 / (Eff * -1)) * 0.95;
                }
                else
                {
                    return Math.Round(Eff * 500 * 0.95, 0);
                }
            }
            else
            {
                return 0;
            }
        }

    }
}