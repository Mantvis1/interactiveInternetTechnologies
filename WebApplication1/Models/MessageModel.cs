namespace WebApplication1.Models
{
    public class MessageModel : BaseModel
    {
        public int userId { get; set; }
        public int messageId { get; set; }
        public string date { get; set; }
        public double money { get; set; }

        public MessageModel(int id, int userId, int messageId, string date, double money) : base(id)
        {
            this.userId = userId;
            this.messageId = messageId;
            this.date = date;
            this.money = money;
        }

        public MessageModel()
        {
        }
    }
}