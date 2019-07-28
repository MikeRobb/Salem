using Salem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salem.Data.Salem;

namespace Salem.Controllers
{
    public class HomeController : Controller
    {
        private readonly SessionRepository _sessionRepo;

        public HomeController()
        {
            _sessionRepo = new SessionRepository();
        }

        public ActionResult Index()
        {
            _sessionRepo.GetGameSessionId();
            return View();
        }

        public ActionResult Players()
        {
            var players = _sessionRepo.GetPlayers();
            return View(players);
        }

        public ActionResult UpdatePlayers(string[] player)
        {
            _sessionRepo.SetPlayers(player);
            return RedirectToAction("Index");
        }

        public ActionResult FirstPlay()
        {
            _sessionRepo.UpdateVotes(new VotesContainer());
            _sessionRepo.StartRound();
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
            _sessionRepo.UpdateIsWitch(isWitch);
            _sessionRepo.UpdateIsConstable(isConstable);

            var players = _sessionRepo.GetPlayers();
            var model = new KillModel(players);
            if(isWitch)
            {
                var m = _sessionRepo.GetVotes();
                model.AddWitchVotes(m.GetWitchVotes());
            }
            return View(model);
        }

        public ActionResult Save(string player)
        {
            if (_sessionRepo.IsWitch())
            {
                var m = _sessionRepo.GetVotes();
                m.SetKilled(player);
                _sessionRepo.UpdateVotes(m);
            }

            var players = _sessionRepo.GetPlayers();
            return View(players);
        }

        public ActionResult AddSave(string player)
        {
            if (_sessionRepo.IsConstable())
            {
                var m = _sessionRepo.GetVotes();
                m.SetSaved(player);
                _sessionRepo.UpdateVotes(m);
            }
            return RedirectToAction("Play");
        }

        public ActionResult Confess()
        {
            var m = _sessionRepo.GetVotes();

            return View(m);
        }

        public ActionResult Results()
        {
            var m = _sessionRepo.GetVotes();
            _sessionRepo.SaveResults(m.GetKilledPlayer(), m.GetSavedPlayer());

            return View(m);
        }
    }
}