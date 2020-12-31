using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
using Microsoft.Extensions.Configuration;
using Client.Repository;

namespace Client.Controllers
{
    public class TeamsController : Controller
    {
        private readonly TeamsRepository contextTeams;
        private readonly PlayersRepository contextPlayers;

        public TeamsController()
        {
            contextTeams = new TeamsRepository();
            contextPlayers = new PlayersRepository();
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");
            return View(await contextTeams.GetAllTeams());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await contextTeams.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,TeamName,TeamRanking,TeamNationality")] Team team)
        {
            if (ModelState.IsValid)
            {
                await contextTeams.AddNewTeam(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        public IActionResult Players(int? id)
        {
            var tm = contextTeams.GetTeamById(id);
            var players = contextPlayers.GetAllPlayers().Result.Where(x => x.TeamId == null || x.TeamId != tm.Result.TeamId);
            if (tm == null || players == null)
                NotFound();
            //
            ViewBag.Team = tm.Result;
            ViewBag.PlayersOnTeam = contextPlayers.GetAllPlayers().Result.Where(x => x.TeamId != null && x.TeamId == tm.Result.TeamId);
            ViewBag.Players = players;
            //
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlayer([Bind("id,TeamId")] int? id, int? PlayerId)
        {
            Team tm = null;
            Player pl = null;


            if (id == null && PlayerId == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                tm = await contextTeams.GetTeamById(id);
                pl = await contextPlayers.GetPlayerById(PlayerId);
                if (tm != null && pl != null)
                {
                    if (!await contextTeams.AddPlayerToTeam(tm, pl))
                    {
                        return RedirectToAction("Players", new { id = id });
                    }
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Adicionar jogador da equipa", Message = e.Message });
            }

            if (tm == null || pl == null)
            {
                return View(NotFound());
            }
            return RedirectToAction("Players", new { id = id });
        }

        public async Task<IActionResult> RemovePlayer(int? id, int? PlayerId)
        {
            Team tm = null;
            Player pl = null;


            if (id == null && PlayerId == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                tm = await contextTeams.GetTeamById(id);
                pl = await contextPlayers.GetPlayerById(PlayerId);
                if (tm != null && pl != null)
                {
                    if (!await contextTeams.RemovePlayerFromTeam(tm, pl))
                    {
                        return RedirectToAction("Players", new { id = id });
                    }
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Remover jogador da equipa", Message = e.Message });
            }

            if (tm == null || pl == null)
            {
                return View(NotFound());
            }
            return RedirectToAction("Players", new { id = id });
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await contextTeams.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,TeamName,TeamRanking,TeamNationality")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await contextTeams.EditTeam(team,id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await contextTeams.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await contextTeams.DeleteTeam(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return contextTeams.GetTeamById(id) != null;
        }
    }
}
