using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.DbContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TournamentManageController : Controller
    {
        private GameDB gDB = new GameDB();
        private MessageDB mDB = new MessageDB();
        private UserDB uDB = new UserDB();
        private RankingDB rDB = new RankingDB();

        public TournamentManageController()
        {
            execute();
            //Task.Run(() => execute());
        }

        private void execute()
        {
            List<int> users = gDB.getAllUsersOfTournament();
            Random rnd = new Random();
            List<Tuple<int, int>> groupA = new List<Tuple<int, int>>((users.Count / 2) + 1);
            List<Tuple<int, int>> groupB = new List<Tuple<int, int>>((users.Count / 2) + 1);
            int halfOfUsersCount = users.Count / 2;
            for (int i = 0; i < halfOfUsersCount; i++)
            {
                int index = rnd.Next(users.Count - 1);
                int? points = gDB.getAllPointsForUserById(users[index]);
                if (points != null)
                {
                    groupA.Add(new Tuple<int, int>(users[index], Convert.ToInt32(points)));
                }
                else
                {
                    groupA.Add(new Tuple<int, int>(users[index], 0));
                }
                users.RemoveAt(index);
                index = rnd.Next(users.Count - 1);
                points = gDB.getAllPointsForUserById(users[index]);
                if (points != null)
                {
                    groupB.Add(new Tuple<int, int>(users[index], Convert.ToInt32(points)));
                }
                else
                {
                    groupB.Add(new Tuple<int, int>(users[index], 0));
                }
                users.RemoveAt(index);
            }

            List<Tuple<int, int>> top4 = new List<Tuple<int, int>>();
            List<Tuple<int, int>> last4 = new List<Tuple<int, int>>();
            for (int i = 0; i < halfOfUsersCount; i++)
            {
                if (groupA[i].Item2 > groupB[i].Item2)
                {
                    top4.Add(new Tuple<int, int>(groupA[i].Item1, groupA[i].Item2));
                    last4.Add(new Tuple<int, int>(groupB[i].Item1, groupB[i].Item2));
                }
                else if (groupA[i].Item2 < groupB[i].Item2)
                {
                    top4.Add(new Tuple<int, int>(groupB[i].Item1, groupB[i].Item2));
                    last4.Add(new Tuple<int, int>(groupA[i].Item1, groupA[i].Item2));
                }
                else
                {
                    top4.Add(new Tuple<int, int>(groupA[i].Item1, groupA[i].Item2));
                    last4.Add(new Tuple<int, int>(groupB[i].Item1, groupB[i].Item2));
                }
            }

            top4 = getRanks(top4);
            last4 = getRanks(last4);

            SendMessagesAndAddPrizes(top4, 1000);
            SendMessagesAndAddPrizes(last4, 100);

            UpdateUserWonAndLostTable(top4, last4);
            clearTournamentTable();

        }


        private void SendMessagesAndAddPrizes(List<Tuple<int, int>> top4, int prize)
        {
            for (int i = 0; i < top4.Count; i++)
            {
                mDB.addNewMessage(top4[i].Item1, 3, Convert.ToInt32(prize));
                double money = uDB.getMoneyById(top4[i].Item1);
                uDB.updateUserMoney(top4[i].Item1, Convert.ToInt32(money + prize));
                prize /= 2;
            }
        }

        private void clearTournamentTable()
        {
            gDB.ClearTournamentTable();
        }

        private void UpdateUserWonAndLostTable(List<Tuple<int, int>> top4, List<Tuple<int, int>> last4)
        {
            for (int i = 0; i < top4.Count; i++)
            {
                if (i == 0)
                {
                    RankingModel rank = rDB.getUserRankinsById(top4[i].Item1);
                    rDB.updateRankings(top4[i].Item1, rank.Win + 3, rank.Lose);
                }
                else if (i == 1 || i == 2)
                {
                    RankingModel rank = rDB.getUserRankinsById(top4[i].Item1);
                    rDB.updateRankings(top4[i].Item1, rank.Win + 2, rank.Lose + 1);
                }
                else if (i == 3)
                {
                    RankingModel rank = rDB.getUserRankinsById(top4[i].Item1);
                    rDB.updateRankings(top4[i].Item1, rank.Win + 1, rank.Lose + 2);
                }
            }

            for (int i = 0; i < last4.Count; i++)
            {
                if (i == 0)
                {
                    RankingModel rank = rDB.getUserRankinsById(last4[i].Item1);
                    rDB.updateRankings(last4[i].Item1, rank.Win + 2, rank.Lose + 1);
                }
                else if (i == 1 || i == 2)
                {
                    RankingModel rank = rDB.getUserRankinsById(last4[i].Item1);
                    rDB.updateRankings(last4[i].Item1, rank.Win + 1, rank.Lose + 2);
                }
                else if (i == 3)
                {
                    RankingModel rank = rDB.getUserRankinsById(last4[i].Item1);
                    rDB.updateRankings(last4[i].Item1, rank.Win, rank.Lose + 3);
                }
            }
        }

        private List<Tuple<int, int>> getRanks(List<Tuple<int, int>> rank)
        {
            for (int i = 1; i <= rank.Count / 2; i++)
            {
                if (rank[i - 1].Item2 < rank[rank.Count - i].Item2)
                {
                    Tuple<int, int> temp = rank[rank.Count - i];
                    rank[rank.Count - i] = rank[i - 1];
                    rank[i - 1] = temp;
                }
            }
            int userIndex = 0;
            for (int i = 1; i <= rank.Count / 2; i++)
            {
                if (rank[userIndex].Item2 < rank[userIndex + 1].Item2)
                {
                    Tuple<int, int> temp = rank[userIndex + 1];
                    rank[userIndex + 1] = rank[userIndex];
                    rank[userIndex] = temp;

                }
                userIndex = +2;
            }
            return rank;
        }

    }
}