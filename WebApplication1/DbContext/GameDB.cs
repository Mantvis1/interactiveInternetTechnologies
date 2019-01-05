using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace WebApplication1.DbContext
{
    public class GameDB : BaseDB
    {
        string query = "";
        public GameDB()
        {

        }

        public int CountOfTournamentPlayer()
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

        public void CreateNewCompetotor(int userId)
        {
            int isExist = isUserExist(userId);
            if (isExist == 0)
            {
                databaseConnection.Open();
                query = "INSERT INTO tournament(id, userId) VALUES (NULL," + userId + ")";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
        }

        private int isUserExist(int userId)
        {
            int count = 0;
            databaseConnection.Open();
            query = "SELECT COUNT(`userId`) FROM `tournament` WHERE userId = " + userId;
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

        public void CreateRankingForUser(int userId)
        {
            databaseConnection.Open();
            query = "INSERT INTO rankings (id, userId, win, loose, value) VALUES(NULL, " + userId + ", 0, 0, 0)";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }

        public List<int> getAllUsersOfTournament()
        {
            List<int> users = new List<int>();
            databaseConnection.Open();
            query = "Select userId from tournament";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    users.Add(reader.GetInt32(0));
                }
            }
            databaseConnection.Close();
            return users;
        }

        public int? getAllPointsForUserById(int id)
        {
            int? points = 0;
            databaseConnection.Open();
            query = "SELECT SUM(points) FROM playerinfo WHERE playerId IN (SELECT playerId from userplayer WHERE userId = " + id + ")";
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

        public void ClearTournamentTable()
        {
            databaseConnection.Open();
            query = "TRUNCATE TABLE tournament";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }
    }
}