using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class UserDB : BaseDB
    {
        string query = "";
        public UserDB()
        {

        }

        public bool AddNewUser(UserModel user)
        {
            int count = 0;
            bool AddedSuccessfully = false;
            if (string.IsNullOrWhiteSpace(user.UserName) != true && string.IsNullOrWhiteSpace(user.Password) != true && string.IsNullOrWhiteSpace(user.Email) != true)
            {
                databaseConnection.Open();
                query = "SELECT COUNT(id) FROM user WHERE Name = '" + user.UserName + "'";
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
                if (count == 0)
                {
                    databaseConnection.Open();
                    query = "INSERT INTO user (id, Name, Password, Email) VALUES (NULL, '" + user.UserName + "', '" + user.Password + "', '" + user.Email + "');";
                    commandDatabase = new MySqlCommand(query, databaseConnection);

                    reader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                    AddedSuccessfully = true;
                }
            }

            return AddedSuccessfully;
        }

        public int CanBeLogedIn(string name, string password)
        {
            int count = 0;
            if (string.IsNullOrWhiteSpace(name) != true && string.IsNullOrWhiteSpace(password) != true)
            {
                databaseConnection.Open();
                query = "select COUNT(id) from user where Name = '" + name + "' and Password = '" + password + "'";
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
            }
            return count;
        }

        public int getUserId(string name, string pass)
        {
            int id = 0;
            if (string.IsNullOrWhiteSpace(name) != true && string.IsNullOrWhiteSpace(pass) != true)
            {
                databaseConnection.Open();
                query = "select id from user where Name = '" + name + "' and Password = '" + pass + "'";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = int.Parse(reader.GetString(0));
                    }
                }
                databaseConnection.Close();
            }

            return id;
        }

        public string getNameById(int userId)
        {
            string name = "";
            if (userId >= 0)
            {
                databaseConnection.Open();
                query = "SELECT name  FROM user WHERE id =" + userId;
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(0);
                    }
                }
                databaseConnection.Close();
            }
            return name;
        }

        public void insertPlayerToUser(int userId, int playerId)
        {
            databaseConnection.Open();
            query = "INSERT INTO userplayer (id, userId, playerId) VALUES (NULL," + userId + "," + playerId + ")";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }
    }
}