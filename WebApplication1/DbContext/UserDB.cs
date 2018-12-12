using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class UserDB : BaseDB
    {
        public UserDB()
        {

        }

        public bool AddNewUser(UserModel user)
        {
            bool AddedSuccessfully = false;
            if (string.IsNullOrWhiteSpace(user.UserName) != true && string.IsNullOrWhiteSpace(user.Password) != true && string.IsNullOrWhiteSpace(user.Email) != true)
            {
                databaseConnection.Open();
                string query = "INSERT INTO user (id, Name, Password, Email) VALUES (NULL, '" + user.UserName + "', '" + user.Password + "', '" + user.Email + "');";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                AddedSuccessfully = true;
            }

            return AddedSuccessfully;
        }

        public int CanBeLogedIn(string name, string password)
        {
            int count = 0;
            string query = "";
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
    }
}