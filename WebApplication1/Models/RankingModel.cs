namespace WebApplication1.Models
{
    public class RankingModel : BaseModel
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Value { get; set; }

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