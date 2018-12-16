using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class PlayerDB : BaseDB
    {
        string query = "";
        public PlayerDB()
        {

        }

        public List<PlayerModel> getAllPlayers()
        {
            List<PlayerModel> players = new List<PlayerModel>();
            nbaDatabaseConnection.Open();
            query = "SELECT * FROM Player";
            MySqlCommand commandDatabase = new MySqlCommand(query, nbaDatabaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    players.Add(new PlayerModel(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            nbaDatabaseConnection.Close();
            return players;
        }

        public void addPlayer(PlayerModel player)
        {
            
            if (player != null)
            {
                databaseConnection.Open();
                var name = new StringBuilder(player.Name).Replace("'", " ");
                query = "INSERT INTO player(id, name) VALUES(" + player.ID + ", '" +name.ToString()+ "')";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }

        }

        public bool clearAllPlayerTable()
        {
            databaseConnection.Open();
            query = "TRUNCATE TABLE player";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            return true;
        }
    }
}