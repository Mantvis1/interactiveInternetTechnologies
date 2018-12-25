using MySql.Data.MySqlClient;
using System.Collections.Generic;
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
                    query = "INSERT INTO user (id, Name, Password, Email,Money) VALUES (NULL, '" + user.UserName + "', '" + user.Password + "', '" + user.Email + "',500);";
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

        public List<int> getUserPlayerIdList(int id)
        {
            List<int> Player = new List<int>();
            databaseConnection.Open();
            query = "SELECT playerId FROM `userplayer` WHERE userId = " + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Player.Add(reader.GetInt32(0));
                }
            }
            return Player;
        }
        public bool DeletePlayerById(int playerId, int userId)
        {
            if (playerId >= 0 && userId >= 0)
            {
                databaseConnection.Open();
                query = "DELETE FROM userplayer WHERE userId = " + userId + " and  playerId = " + playerId;
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int isUserHavePlayerById(int playerId)
        {
            if (playerId >= 0)
            {
                int count = -1;
                databaseConnection.Open();
                query = "SELECT Count(id) FROM userplayer WHERE `playerId`=" + playerId;
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                databaseConnection.Close();
                return count;
            }
            else
            {
                return -1;
            }
        }

        public int getMoneyById(int id)
        {
            int money = 0;
            databaseConnection.Open();
            query = "SELECT money from user where id = " + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    money = reader.GetInt32(0);
                }
            }
            databaseConnection.Close();
            return money;
        }

        public void updateUserMoney(int id, int result)
        {
            databaseConnection.Open();
            query = "UPDATE user SET money =" + result + " WHERE id =" + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }
    }
}