﻿namespace WebApplication1.Models
{
    public class MessageViewModel : BaseModel
    {
        public int userId { get; set; }
        public string text { get; set; }
        public string date { get; set; }
        public double money { get; set; }

        public MessageViewModel(int id, int userId, string text, string date, double money) : base(id)
        {
            this.userId = userId;
            this.text = text;
            this.date = date;
            this.money = money;
        }

        public MessageViewModel()
        {
        }
    }
}