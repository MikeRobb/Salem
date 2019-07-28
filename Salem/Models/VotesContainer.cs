using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Salem.Models
{
    public class VotesContainer
    {
        private string PlayerSaved;
        private string[] PlayersKilled;

        public VotesContainer()
        {
            PlayerSaved = string.Empty;
            PlayersKilled = new string[0];
        }

        public void SetSaved(string player)
        {
            PlayerSaved = player;
        }

        public void SetKilled(string player)
        {
            bool found = false;
            for(int i = 0; i < PlayersKilled.Length; i++)
            {
                string cur = PlayersKilled[i];
                if(cur.Equals(player))
                { found = true; continue; }
            }

            if(found == false)
            {
                var temp = new string[PlayersKilled.Length + 1];
                int i = 0;
                foreach(var p in PlayersKilled)
                {
                    temp[i] = p;
                    i++;
                }

                temp[i] = player;
                PlayersKilled = temp.ToArray();
            }
        }

        public string GetSavedPlayer()
        {
            return PlayerSaved;
        }

        public string GetKilledPlayer()
        {
            var r = string.Empty;
            int curMaxCount = 0;
            string curMax = string.Empty;
            var dict = new Dictionary<string, int>();
            foreach(var p in PlayersKilled)
            {
                if (dict.ContainsKey(p) == false)
                    dict[p] = 0;

                var curCount = dict[p] + 1;
                if(curCount >= curMaxCount)
                {
                    curMax = p;
                    curMaxCount = curCount;
                }

                dict[p] = curCount;
            }
            return curMax; ;
        }
    }
}