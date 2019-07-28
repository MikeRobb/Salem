using Salem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salem.Controllers
{
    public class HomeController : Controller
    {
        private const string VotesKey = "Votes";
        private const string PlayersKey = "Players";
        private const string WitchKey = "Witch";
        private const string ConstableKey = "Constable"; 

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Players()
        {
            var players = Session[PlayersKey];
            if(players == null)
            {
                players = new string[0];
            }
            return View(players);
        }

        public ActionResult UpdatePlayers(string[] player)
        {
            Session[PlayersKey] = player;
            return RedirectToAction("Index");
        }

        public ActionResult FirstPlay()
        {
            Session[VotesKey] = new VotesContainer();
            return RedirectToAction("Play");
        }

        public ActionResult Play()
        {
            return View();
        }

        public ActionResult Role()
        {
            return View();
        }

        public ActionResult Kill(bool isWitch, bool isConstable)
        {
            Session[WitchKey] = isWitch;
            Session[ConstableKey] = isConstable;

            var players = (string[])Session[PlayersKey] ?? new string[0];
            var model = new KillModel(players);
            if(isWitch)
            {
                var m = (VotesContainer)Session[VotesKey];
                model.AddWitchVotes(m.GetWitchVotes());
            }
            return View(model);
        }

        public ActionResult Save(string player)
        {
            if ((bool)Session[WitchKey])
            {
                var m = (VotesContainer)Session[VotesKey];
                m.SetKilled(player);
                Session[VotesKey] = m;
            }

            var players = (string[])Session[PlayersKey] ?? new string[0];
            return View(players);
        }

        public ActionResult AddSave(string player)
        {
            if ((bool)Session[ConstableKey])
            {
                var m = (VotesContainer)Session[VotesKey];
                m.SetSaved(player);
                Session[VotesKey] = m;
            }
            return RedirectToAction("Play");
        }

        public ActionResult Confess()
        {
            var m = (VotesContainer)Session[VotesKey];

            return View(m);
        }

        public ActionResult Results()
        {
            var m = (VotesContainer)Session[VotesKey];

            return View(m);
        }
    }
}