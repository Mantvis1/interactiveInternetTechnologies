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

        public void AddNewUser(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) != true && string.IsNullOrWhiteSpace(user.Password) != true && string.IsNullOrWhiteSpace(user.Email) != true)
            {
                databaseConnection.Open();
                string query = "INSERT INTO user (id, Name, Password, Email) VALUES (NULL, '"+user.UserName+"', '"+user.Password+"', '"+user.Email+"');";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                //commandDatabase.CommandTimeout = 60;
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                //databaseConnection.Close();

            }
        }

       /* public int CanBeLogedIn(string name, string password)
        {
            int count = 0;
            string query = "select COUNT(id) from Users where Users.Name = '" + name + "' and Users.Password = '" + password + "'";
            SqlDataAdapter adapter = new SqlDataAdapter();

            if (string.IsNullOrWhiteSpace(name) != true && string.IsNullOrWhiteSpace(password) != true)
            {
                cnn.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(query, cnn);
                adapter.SelectCommand = cmd;
                count = adapter.Fill(ds);
                cnn.Close();
            }
            return count;
        }*/
    }
}