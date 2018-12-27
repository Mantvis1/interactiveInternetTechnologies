using System;
using MySql.Data.MySqlClient;

namespace WebApplication1.DbContext
{
    public class ManageDB : BaseDB
    {
        string query = "";

        public ManageDB()
        {

        }

        public bool UpdatePassword(int id, string password)
        {
            databaseConnection.Open();
            query = "UPDATE user SET Password = '"+password+"' where id ="+id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            return true;
        }

        public void UpdateUsername(int id, string userName)
        {
            databaseConnection.Open();
            query = "UPDATE user SET Name = '" + userName + "' where id =" + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }

        public void UpdateEmail(int id, string email)
        {
            databaseConnection.Open();
            query = "UPDATE user SET Email = '" + email + "' where id =" + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }

        public void DeleteAccount(int id)
        {
            databaseConnection.Open();
            query = "DELETE FROM userplayer WHERE userId =" + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            databaseConnection.Open();
            query = "DELETE FROM rankings WHERE userId =" + id;
            commandDatabase = new MySqlCommand(query, databaseConnection);
            reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            databaseConnection.Open();
            query = "DELETE FROM tournament WHERE userId =" + id;
            commandDatabase = new MySqlCommand(query, databaseConnection);
            reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            databaseConnection.Open();
            query = "DELETE FROM message WHERE userId =" + id;
            commandDatabase = new MySqlCommand(query, databaseConnection);
            reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            databaseConnection.Open();
            query = "DELETE FROM user WHERE id =" + id;
            commandDatabase = new MySqlCommand(query, databaseConnection);
            reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }

        public bool isUserExists(int id, string password)
        {
            int count = 0;
            databaseConnection.Open();
            query = "SELECT count(id) FROM user WHERE id = "+id+" and Password = '"+password+"'";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    count = int.Parse(reader.GetString(0));
                }
            }
            databaseConnection.Close();
            if (count == 1)
                return true;
            else
                return false;
        }

        public void addNewMessage(int userId, string message, double cost)
        {
            databaseConnection.Open();
            string time = Convert.ToString(DateTime.Now);
            query = "INSERT INTO message(id, userId, messageId, time, money) VALUES (NULL,"+userId+",'"+message+"','"+time+"',"+cost+")";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        
        

        }
    }
}