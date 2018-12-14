namespace WebApplication1.Models
{
    public class RankingModel : BaseModel
    {
        public string Name;
        public int Win;
        public int Lose;
        public int Value;

        public RankingModel(string Name, int Win, int Lose, int Value)
        {
            this.Name = Name;
            this.Win = Win;
            this.Lose = Lose;
            this.Value = Value;
        }

        public int AllGames()
        {
            return Win + Lose;
        }
    }
}