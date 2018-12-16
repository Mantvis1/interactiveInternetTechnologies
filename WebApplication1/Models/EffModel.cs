using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EffModel :BaseModel
    {
        public int pts;
        public int eff;

        public EffModel(int pts, int eff) : base()
        {
            this.pts = pts;
            this.eff = eff;
        }

        public EffModel()
        {
        }
    }
}