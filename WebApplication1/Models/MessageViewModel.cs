namespace WebApplication1.Models
{
    public class MessageViewModel : BaseModel
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public double Money { get; set; }

        public MessageViewModel(int id, int userId, string text, string date, double money) : base(id)
        {
            UserId = userId;
            Text = text;
            Date = date;
            Money = money;
        }

        public MessageViewModel()
        {
        }
    }
}