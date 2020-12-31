using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class GamesController : Controller
    {
        GamesRepository contextGames = null;
        EventsRepository contextEvents = null;

        public GamesController()
        {
            contextGames = new GamesRepository();
            contextEvents = new EventsRepository();
        }

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                ViewBag.Event = await contextEvents.GetEventById(id);
                ViewBag.Games = await contextGames.GetGameByEventId(id);
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Listagem de jogos", Message = e.Message });
            }

            if (ViewBag.Event == null || ViewBag.Games == null)
            {
                return NotFound();
            }
            //
            return View();
        }

        public IActionResult Create(int? id)
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");
            //
            var ev = contextEvents.GetEventById(id);
            List<Team> teamA = ev.Result.Teams.AsEnumerable().ToList();
            List<Team> teamB = ev.Result.Teams.AsEnumerable().ToList();
            teamA.RemoveAt(1);
            teamB.RemoveAt(0);
            ViewBag.Event = ev.Result;
            ViewBag.TeamsA = new SelectList(teamA, nameof(Team.TeamId), nameof(Team.TeamName));
            ViewBag.TeamsB = new SelectList(teamB, nameof(Team.TeamId), nameof(Team.TeamName));
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("EventId,GameId,GameDate,TeamAId,TeamBId,TeamWinnerId")] Game @game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextGames.AddNewGame(@game);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Novo Jogo", Message = e.Message });
            }

            return View(@game);
        }

        public async Task<IActionResult> Maps(int? id)
        {
            return null;
        }

        public async Task<IActionResult> Details(int? id)
        {
            Game gm = null;


            if (id == null)
            {
                return View(NotFound());
            }
            try
            {
                gm = await contextGames.GetGameById(id);
                //gm.MapOfGames = await contextGames.GetGameByEventId(id);
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Listagem da competição", Message = e.Message });
            }

            if (gm == null || gm.MapOfGames == null)
            {
                return View(NotFound());
            }

            return View(gm);
        }

        [HttpPost]
        public async Task<IEnumerable<object>> GetTeamsExcluded([Bind("id,team")]int? id, int team)
        {
            if (Program.Token == null || Program.Authentication == null)
            {
                RedirectToAction("Home/Index");
                return null;
            }
                

            if (id == null || team == null || team == 0)
                return null;

            var auxTeams = contextEvents.GetEventById(id).Result.Teams;
            var teams = from t in auxTeams
                        where t.TeamId != team
                        select new { Value = t.TeamId, Text = t.TeamName};

            return teams;

        }

        public IActionResult Edit(int? id)
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");
            //
            var gm = contextGames.GetGameById(id);
            var ev = gm.Result.Event;
            List<Team> teamA = ev.Teams.AsEnumerable().ToList();
            List<Team> teamB = ev.Teams.AsEnumerable().ToList();
            teamA.Remove(teamA.FirstOrDefault(x => x.TeamId == gm.Result.TeamBId));
            teamB.Remove(teamB.FirstOrDefault(x => x.TeamId == gm.Result.TeamAId));
            ViewBag.Event = ev;
            ViewBag.TeamsA = new SelectList(teamA, nameof(Team.TeamId), nameof(Team.TeamName));
            ViewBag.TeamsB = new SelectList(teamB, nameof(Team.TeamId), nameof(Team.TeamName));

            //
            if (gm.Result.TeamWinnerId == null)
                gm.Result.TeamWinnerId = 0;

            return View(gm.Result);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("EventId,GameId,GameDate,TeamAId,TeamBId,TeamWinnerId")] Game @game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextGames.EditGame(@game,id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Editar Jogo", Message = e.Message });
            }

            return View(@game);
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
