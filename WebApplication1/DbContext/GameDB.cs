using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DbContext
{
    public class GameDB : BaseDB
    {
        string query = "";
        public GameDB()
        {

        }

        public int CountOfTournamentPlayers()
        {
            int count = 0;
            databaseConnection.Open();
            query = "Select COUNT(id) from tournament";
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
            return count;
        }
    }
}