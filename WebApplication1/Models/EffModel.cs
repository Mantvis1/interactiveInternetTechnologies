namespace WebApplication1.Models
{
    public class EffModel : BaseModel
    {
        public double Pts { get; set; }
        public double Eff { get; set; }

        public EffModel(double pts, double eff) : base()
        {
            Pts = pts;
            Eff = eff;
        }

        public EffModel()
        {
        }
    }
}