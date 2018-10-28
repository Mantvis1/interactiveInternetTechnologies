using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DbContext
{
    public class PlayerDB : BaseDB
    {   
        public PlayerDB()
        {

        }

        public List<PlayerModel> getAllPlayers()
        {
            List<PlayerModel> listOfObjects = new List<PlayerModel>();
            string query = "select Players.Name,Players.Surname,PlayersResult.Points,PlayersResult.Effectivity from Players INNER JOIN PlayersResult ON Players.Id = PlayersResult.PlayerID order by PlayersResult.Effectivity desc";
            using (SqlCommand command = new SqlCommand(query, cnn))
            {
                cnn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PlayerModel P = new PlayerModel(reader["Name"].ToString(), reader["Surname"].ToString(), Convert.ToInt32(reader["Points"]), Convert.ToDouble(reader["Effectivity"]));
                        listOfObjects.Add(P);
                    }
                    cnn.Close();
                }
                return listOfObjects;
            }

        }
    }
}