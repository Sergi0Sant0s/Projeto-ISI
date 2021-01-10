using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
                return View(await contextTeams.GetAllTeams(HttpContext));
        }

        // GET: Teams/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Detalhes da equipa", Message = e.Message });
            }
        }

        // GET: Teams/Create
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([Bind("TeamId,TeamName,TeamRanking,TeamNationality")] Team team)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextTeams.AddNewTeam(HttpContext, team);
                    return RedirectToAction(nameof(Index));
                }
                return View(team);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Criação da equipa", Message = e.Message });
            }
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Players(int? id)
        {
            try
            {
                var tm = contextTeams.GetTeamById(id);
                var players = contextPlayers.GetAllPlayers();
                if (tm == null || players == null)
                    NotFound();
                //
                ViewBag.Team = tm.Result;
                ViewBag.PlayersOnTeam = players.Result.Where(x => x.TeamId != null && x.TeamId == tm.Result.TeamId);
                ViewBag.Players = players.Result.Where(x => x.TeamId == null || x.TeamId != tm.Result.TeamId).Where(x=>x.TeamId == null).ToList();
                //
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Inserção de jogadores na equipa", Message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
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
                    if (!await contextTeams.AddPlayerToTeam(HttpContext, tm, pl))
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

        [Authorize(Roles = "Admin,User")]
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
                    if (!await contextTeams.RemovePlayerFromTeam(HttpContext, tm, pl))
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Editar equipa", Message = e.Message });
            }
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
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
                    await contextTeams.EditTeam(HttpContext, team,id);
                }
                catch (Exception e)
                {
                    if (!TeamExists(team.TeamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return View("Error", new Error() { Title = "Editar equipa", Message = e.Message });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar equipa", Message = e.Message });
            }
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var team = await contextTeams.DeleteTeam(HttpContext, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar equipa", Message = e.Message });
            }
        }

        [Authorize(Roles = "Admin,User")]
        private bool TeamExists(int id)
        {
            return contextTeams.GetTeamById(id) != null;
        }
    }
}
