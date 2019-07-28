using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Salem.Models
{
    public class VotesContainer
    {
        private string PlayerSaved;
        private IList<string> PlayersKilled;

        public VotesContainer()
        {
            PlayerSaved = string.Empty;
            PlayersKilled = new List<string>();
        }

        public void SetSaved(string player)
        {
            PlayerSaved = player;
        }

        public void SetKilled(string player)
        {
            PlayersKilled.Add(player);
        }

        public string GetSavedPlayer()
        {
            return PlayerSaved;
        }

        public IDictionary<string, int> GetWitchVotes()
        {
            var dict = new Dictionary<string, int>();
            foreach (var p in PlayersKilled)
            {
                if (dict.ContainsKey(p) == false)
                    dict[p] = 0;

                dict[p] += 1;
            }

            return dict;
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