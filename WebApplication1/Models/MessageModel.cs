﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MessageModel : BaseModel
    {
        public int userId { get; set; }
        public int messageId { get; set; }
        public DateTime date { get; set; }
        public double money { get; set; }

        public MessageModel (int id, int userId, int messageId, DateTime date, double money) : base(id)
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