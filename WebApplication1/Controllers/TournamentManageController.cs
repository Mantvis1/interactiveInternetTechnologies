using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbContext;

namespace WebApplication1.Controllers
{
    public class TournamentManageController : Controller
    {
        GameDB GDB = new GameDB();
        MessageDB MDB = new MessageDB();
        UserDB UDB = new UserDB();

        public TournamentManageController()
        {
            execute();
        }

        private void execute()
        {
            List<int> users = GDB.getAllUsersOfTournament();
            Random rnd = new Random();
            List<Tuple<int, int>> groupA = new List<Tuple<int, int>>((users.Count / 2) + 1);
            List<Tuple<int, int>> groupB = new List<Tuple<int, int>>((users.Count / 2) + 1);
            int halfOfUsersCount = users.Count / 2;
            for (int i = 0; i < halfOfUsersCount; i++)
            {
                int index = rnd.Next(users.Count - 1);
                int? points = GDB.getAllPointsForUserById(users[index]);
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
                points = GDB.getAllPointsForUserById(users[index]);
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

            List<Tuple<int,int>> top4 = new List<Tuple<int, int>>();
            for (int i = 0; i < halfOfUsersCount; i++)
            {
                if (groupA[i].Item2 > groupB[i].Item2)
                {
                    top4.Add(new Tuple<int,int>(groupA[i].Item1,groupA[i].Item2));
                }
                else if (groupA[i].Item2 < groupB[i].Item2)
                {
                    top4.Add(new Tuple<int, int>(groupB[i].Item1, groupB[i].Item2));
                }
                else
                {
                    top4.Add(new Tuple<int, int>(groupA[i].Item1, groupA[i].Item2));
                }
            }


            for (int i = 1; i <= halfOfUsersCount/2; i++)
            {
                if(top4[i-1].Item2 < top4[top4.Count - i].Item2)
                {
                    Tuple<int, int> temp = top4[top4.Count - i];
                    top4[top4.Count - i] = top4[i-1];
                    top4[i - 1] = temp;
                }
            }
            int userIndex = 0;
            for (int i = 1; i <= halfOfUsersCount / 2; i++)
            {
                if (top4[userIndex].Item2 < top4[userIndex+1].Item2)
                {
                    Tuple<int, int> temp = top4[userIndex+1];
                    top4[userIndex+1] = top4[userIndex];
                    top4[userIndex] = temp;
                    
                }
                userIndex = +2;
            }

            SendMessagesAndAddPrizes(top4);
        }

        private void SendMessagesAndAddPrizes(List<Tuple<int, int>> top4)
        {
            int prize = 1000;
            for (int i = 0; i < top4.Count; i++)
            {
                MDB.addNewMessage(top4[i].Item1, 3, Convert.ToInt32(prize));
                double money = UDB.getMoneyById(top4[i].Item1);
                UDB.updateUserMoney(top4[i].Item1, Convert.ToInt32(money + prize));
                prize /= 2;
            }
          //  Session["money"] = UDB.getMoneyById((int)Session["id"]);
        }

    }
}