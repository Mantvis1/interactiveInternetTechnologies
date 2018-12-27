using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MessageViewModel : BaseModel
    {
        public int userId { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public double money { get; set; }

        public MessageViewModel(int id, int userId, string text, DateTime date, double money) : base(id)
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