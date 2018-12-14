﻿using MySql.Data.MySqlClient;
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

        public bool CreateNewCompetotor(int userId)
        {
            int isExist = isUserExist(userId);
            if(isExist == 0)
            {
                databaseConnection.Open();
                query = "INSERT INTO tournament(id, userId) VALUES (NULL," + userId + ")";
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                return true;
            } else
            {
                return false;
            }
        }

        private int isUserExist(int userId)
        {
            int count = 0;
            databaseConnection.Open(); ;
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
    }
}