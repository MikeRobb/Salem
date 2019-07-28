using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Salem.Models
{
    public class KillModel
    {
        public string[] Players;
        public IDictionary<string, int> WitchVotes;

        public KillModel(string[] players)
        {
            Players = players;
            WitchVotes = new Dictionary<string, int>();
        }

        public void AddWitchVotes(IDictionary<string, int> witchVotes)
        {
            WitchVotes = witchVotes;
        }
    }
}