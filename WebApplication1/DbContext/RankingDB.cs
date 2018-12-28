using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class RankingDB : BaseDB
    {
        string query = "";
        UserDB user = new UserDB();

        public RankingDB()
        {

        }

        public List<RankingModel> getAllRankings()
        {
            int rank = 1;
            List<RankingModel> rankings = new List<RankingModel>();
            databaseConnection.Open();
            query = "SELECT * FROM rankings ORDER BY win /(win + loose) DESC";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string name = user.getNameById(reader.GetInt32(1));
                    rankings.Add(new RankingModel(rank++, name, reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
                }
            }
            databaseConnection.Close();
            return rankings;
        }

        public RankingModel getUserRankinsById(int id)
        {
            RankingModel rank = new RankingModel();
            databaseConnection.Open();
            query = "Select win,loose from rankings where userId = " + id; ;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rank = new RankingModel(0, "", reader.GetInt32(0), reader.GetInt32(1), 0);

                }
            }
            databaseConnection.Close();
            return rank;
        }

        public void updateRankings(int userId, int win, int lose)
        {
            databaseConnection.Open();
            query = "UPDATE rankings SET win= " + win + ", loose= " + lose + " WHERE userid = " + userId;
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }
    }
}