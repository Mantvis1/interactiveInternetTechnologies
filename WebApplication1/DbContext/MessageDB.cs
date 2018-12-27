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
            if (id > -1)
            {
                databaseConnection.Open();
                query = "Select * from message where userId =" + id;
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MessageModel model = new MessageModel(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetDateTime(3), reader.GetDouble(4));
                        messages.Add(model);
                    }
                }
                databaseConnection.Close();
            }
            return messages;
        }

        public void deleteMessageById(int messageId)
        {
            databaseConnection.Open();
            query = "Delete from message where id = " + messageId;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }

        public void deleteAllMessages(int id)
        {
            databaseConnection.Open();
            query = "Delete from message where userID = " + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }

        public string GetMessageTypeById(int id)
        {
            string message = "";
            if (id >= 0)
            {
                databaseConnection.Open();
                query = "SELECT message FROM messagetype WHERE id=" + id;
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        message = reader.GetString(0);
                    }
                }
                databaseConnection.Close();
            }
            return message;
        }
    }
}