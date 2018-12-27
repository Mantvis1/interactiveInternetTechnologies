using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MessageModel : BaseModel
    {
        public int userId { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }

        public MessageModel (int id, int userId, string text, DateTime date) : base(id)
        {
            this.userId = userId;
            this.text = text;
            this.date = date;
        }

        public MessageModel()
        {
        }
    }
}