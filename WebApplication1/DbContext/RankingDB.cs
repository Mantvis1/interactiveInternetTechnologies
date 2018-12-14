using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public List<RankingModel> getAllRankings(int start, int end)
        {
            List<RankingModel> rankings = new List<RankingModel>();
            
            if(end > 0 && start >= 0 && end > start)
            {
                databaseConnection.Open();
                query = "SELECT * FROM rankings LImit "+start+","+(end-start)+";";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = user.getNameById(reader.GetInt32(1));
                        rankings.Add(new RankingModel(name, reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                }
                databaseConnection.Close();
            }
            return rankings;
        }
    }
}