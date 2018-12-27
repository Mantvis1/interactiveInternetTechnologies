using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class MessageDB : BaseDB
    {
        private string query = "";

        public List<MessageModel> getAllUserMessages(int id)
        {
            List<MessageModel> messages = new List<MessageModel>();
            if(id > -1)
            {
                databaseConnection.Open();
                query = "Select * from message where userId =" + id;
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MessageModel model = new MessageModel(reader.GetInt32(0), reader.GetInt32(1),reader.GetString(2),reader.GetDateTime(3));
                        messages.Add(model);
                    }
                }
                databaseConnection.Close();
            }

            return messages;
        }
    }
}