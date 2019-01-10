namespace WebApplication1.Models
{
    public class MessageModel : BaseModel
    {
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public string Date { get; set; }
        public double Money { get; set; }

        public MessageModel(int id, int userId, int messageId, string date, double money) : base(id)
        {
            UserId = userId;
            MessageId = messageId;
            Date = date;
            Money = money;
        }

        public MessageModel()
        {
        }
    }
}