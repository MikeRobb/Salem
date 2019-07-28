using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Salem.Data.Salem;

namespace Salem.Models
{
    public class SessionRepository
    {
        public HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        private const string GameSessionIdKey = "GameSessionId";
        private const string RoundKey = "Round";
        private const string VotesKey = "Votes";
        private const string PlayersKey = "Players";
        private const string WitchKey = "Witch";
        private const string ConstableKey = "Constable";

        public int GetGameSessionId()
        {
            int id = -1;
            if (Session[GameSessionIdKey] != null)
                return (int)Session[GameSessionIdKey];
            using (var db = new SalemEntities())
            {
                var tomorrow = DateTime.Today.AddDays(1);
                var curGameSession = db.GameSessions.FirstOrDefault(x => x.StartTime >= DateTime.Today && x.StartTime < tomorrow);
                if (curGameSession == null)
                {
                    curGameSession = new GameSession { StartTime = DateTime.Now };
                    db.GameSessions.Add(curGameSession);
                    db.SaveChanges();
                }

                Session[GameSessionIdKey] = curGameSession.Id;
            }

            return id;
        }

        public string[] GetPlayers()
        {
            var players = (string[])Session[PlayersKey];
            if (players == null)
            {
                players = new string[0];
            }
            return players;
        }

        public void SetPlayers(string[] players)
        {
            Session[PlayersKey] = players;
        }

        public void UpdateVotes(VotesContainer votes)
        {
            Session[VotesKey] = votes;
        }

        public void StartRound()
        {
            using (var db = new SalemEntities())
            {
                var gameSessionId = GetGameSessionId();
                var r = new Round
                {
                    GameSessionId = gameSessionId,
                    StartTime = DateTime.Now,
                };
                db.Rounds.Add(r);
                db.SaveChanges();
                Session[RoundKey] = r.Id;
            }
        }

        public int GetRoundId()
        {
            return (int)Session[RoundKey];
        }

        public void SaveResults(string killedPlayer, string savedPlayer)
        {
            using (var db = new SalemEntities())
            {
                var roundId = GetRoundId();
                var round = db.Rounds.First(x => x.Id == roundId);
                round.EndTime = DateTime.Now;
                round.KillVote = killedPlayer;
                round.SaveVote = savedPlayer;
                db.SaveChanges();
            }
        }

        public VotesContainer GetVotes()
        {
            return (VotesContainer)Session[VotesKey];
        }

        public void UpdateIsWitch(bool isWitch)
        {
            Session[WitchKey] = isWitch;
        }

        public bool IsWitch()
        {
            return (bool)Session[WitchKey];
        }

        public void UpdateIsConstable(bool isConstable)
        {
            Session[ConstableKey] = isConstable;
        }

        public bool IsConstable()
        {
            return (bool)Session[ConstableKey];
        }
    }
}