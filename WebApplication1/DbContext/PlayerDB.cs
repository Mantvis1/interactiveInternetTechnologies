using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class PlayerDB : BaseDB
    {
        string query = "";
        PlayerManagerController manager = new PlayerManagerController();
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
                query = "INSERT INTO player(id, name) VALUES(" + player.ID + ", '" + name.ToString() + "')";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
        }

        public bool clearAllPlayerTable()
        {
            databaseConnection.Open();
            query = "ALTER TABLE player AUTO_INCREMENT = 1";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            return true;
        }

        public EffModel getEff(int id)
        {
            if (id > -1)
            {
                EffModel ef = new EffModel();
                nbaDatabaseConnection.Open();
                query = "SELECT * FROM Actions WHERE PlayerId =" + id + " ORDER by PlayerId DESC LIMIT 5";
                MySqlCommand commandDatabase = new MySqlCommand(query, nbaDatabaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int points = reader.GetInt32(20);
                        ef.ID = id;
                        ef.pts = points;
                        ef.eff = manager.playerEff(points, reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(16), reader.GetInt32(18), reader.GetInt32(5) - reader.GetInt32(4), reader.GetInt32(9) - reader.GetInt32(8), reader.GetInt32(17));
                    }
                }
                nbaDatabaseConnection.Close();
                return ef;
            }
            else
            {
                return null;
            }
        }

        public bool clearAllEffTable()
        {
            databaseConnection.Open();
            query = "TRUNCATE TABLE playerInfo";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
            return true;
        }

        public void addPlayerInfo(EffModel player)
        {
            if (player != null)
            {
                databaseConnection.Open();
                query = "INSERT INTO playerinfo(id, playerId, points, eff) VALUES (NULL," + player.ID + "," + player.pts + "," + player.eff + ")";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
        }

    }
}