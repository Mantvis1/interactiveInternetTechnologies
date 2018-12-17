using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EffModel :BaseModel
    {
        public double pts;
        public double eff;

        public EffModel(double pts, double eff) : base()
        {
            this.pts = pts;
            this.eff = eff;
        }

        public EffModel()
        {
        }
    }
}