namespace WebApplication1.Models
{
    public class RankingModel : BaseModel
    {
        public int Rank;
        public string Name;
        public int Win;
        public int Lose;
        public int Value;

        public RankingModel(int Rank, string Name, int Win, int Lose, int Value)
        {
            this.Rank = Rank;
            this.Name = Name;
            this.Win = Win;
            this.Lose = Lose;
            this.Value = Value;
        }

        public RankingModel()
        {
        }

        public int AllGames()
        {
            return Win + Lose;
        }
    }
}