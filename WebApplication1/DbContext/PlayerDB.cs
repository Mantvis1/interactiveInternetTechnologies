using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.DbContext
{
    public class PlayerDB : BaseDB
    {
        string query = "";
        PlayerManagerController pMC = new PlayerManagerController();

        public PlayerDB()
        {

        }

        public List<PlayerModel> getAllPlayers()
        {
            List<PlayerModel> Player = new List<PlayerModel>();
            nbaDatabaseConnection.Open();
            query = "SELECT * FROM Player";
            MySqlCommand commandDatabase = new MySqlCommand(query, nbaDatabaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Player.Add(new PlayerModel(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            nbaDatabaseConnection.Close();
            return Player;
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
                int limit = 5;
                nbaDatabaseConnection.Open();
                query = "SELECT * FROM Actions WHERE PlayerId =" + id + " ORDER by PlayerId DESC LIMIT " + limit;
                MySqlCommand commandDatabase = new MySqlCommand(query, nbaDatabaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    int points = 0;
                    int eff = 0;
                    while (reader.Read())
                    {
                        points += reader.GetInt32(20);
                        eff += pMC.playerEff(points, reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(16), reader.GetInt32(18), reader.GetInt32(5) - reader.GetInt32(4), reader.GetInt32(9) - reader.GetInt32(8), reader.GetInt32(17));
                    }
                    ef.ID = id;
                    ef.Pts = points / limit;
                    ef.Eff = eff / limit;
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
                query = "INSERT INTO playerinfo(id, playerId, points, eff) VALUES (NULL," + player.ID + "," + player.Pts + "," + player.Eff + ")";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
        }

        public string getPlayerById(int id)
        {
            string name = "";
            if (id >= 0)
            {
                databaseConnection.Open();
                query = "SELECT name  FROM player WHERE id =" + id;
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
                return name;
            }
            else
            {
                return "";
            }
        }

        public List<PlayerModel> getAllLocalPlayers()
        {
            List<PlayerModel> Player = new List<PlayerModel>();
            databaseConnection.Open();
            query = "SELECT * FROM Player"; //  test -> limit 35" ;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Player.Add(new PlayerModel(reader.GetInt32(0), reader.GetString(1)));
                }
            }
            databaseConnection.Close();
            return Player;
        }

        public EffModel getPointsAndEff(int id)
        {
            EffModel eff = new EffModel();
            databaseConnection.Open();
            query = "SELECT points, eff FROM playerinfo WHERE id = " + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    eff.Eff = reader.GetDouble(1);
                    eff.Pts = reader.GetDouble(0);
                }
            }
            databaseConnection.Close();
            return eff;
        }

        public double getPlayerPointsById(int id)
        {
            double points = 0;
            databaseConnection.Open();
            query = "Select points from playerinfo where id =" + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    points = reader.GetInt32(0);
                }
            }
            databaseConnection.Close();
            return points;
        }

        public double getEffById(int id)
        {
            double eff = 0;
            databaseConnection.Open();
            query = "Select eff from playerinfo where id =" + id;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    eff = reader.GetInt32(0);
                }
            }
            databaseConnection.Close();
            return eff;
        }
    }
}