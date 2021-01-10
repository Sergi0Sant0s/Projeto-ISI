using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATA.Context;
using DATA.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Server.Controllers
{
    [Authorize("Bearer")]
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class TeamsController : ControllerBase
    {
        private readonly MyContext _context;

        public TeamsController(MyContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Devolve todas as equipas
        /// </summary>
        /// <returns>Retorna uma lista com todas as equipas</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }


        /// <summary>
        /// Devolve uma equipa pelo seu id
        /// </summary>
        /// <param name="id">id da equipa</param>
        /// <returns>Retorna um objeto do tipo equipa</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetTeam", new { id = @team.TeamId }, @team);
        }


        /// <summary>
        /// Edita uma equipa
        /// </summary>
        /// <param name="id">id da equipa</param>
        /// <param name="team">objeto com a equipa alterada</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.TeamId)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        /// <summary>
        /// Adiciona nova equipa
        /// </summary>
        /// <param name="team">objeto com a nova equipa</param>
        /// <returns>Retorna um objeto com a nova equipa</returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team @team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = @team.TeamId }, @team);
        }


        /// <summary>
        /// Adiciona um jogado a uma equipa
        /// </summary>
        /// <param name="idTeam">id da equipa</param>
        /// <param name="idPlayer">id do jogador</param>
        /// <returns>Retorna true/false</returns>
        [HttpPost("AddPlayerToTeam")]
        [Authorize("Bearer")]
        public async Task<bool> AddPlayerToTeam(int idTeam, int idPlayer)
        {
            try
            {
                var tm = await _context.Teams.FindAsync(idTeam);
                var pl = await _context.Players.FindAsync(idPlayer);
                if (tm == null || pl == null)
                    NotFound();
                //Insert
                //_context.Teams.Include("Players").FirstOrDefault(x => x.TeamId == idTeam).Players.Add(pl);
                _context.Players.FirstOrDefault(x => x.PlayerId == idPlayer).Team = tm;
                //Save
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// Remove jogador da equipa
        /// </summary>
        /// <param name="idTeam">id da equipa</param>
        /// <param name="idPlayer">id do jogador</param>
        /// <returns>Retorna true/false</returns>
        [HttpPost("RemovePlayerFromTeam")]
        [Authorize("Bearer")]
        public async Task<bool> RemovePlayerFromTeam(int idTeam, int idPlayer)
        {
            try
            {
                var tm = await _context.Teams.FindAsync(idTeam);
                var pl = await _context.Players.FindAsync(idPlayer);
                if (tm == null || pl == null)
                    NotFound();
                //Insert
                //_context.Teams.Include("Players").FirstOrDefault(x => x.TeamId == idTeam).Players.Remove(pl);
                _context.Players.FirstOrDefault(x => x.PlayerId == idPlayer).Team = null;
                //Save
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// Elimina equipa pelo seu id
        /// </summary>
        /// <param name="id">id da equipa</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Verifica se a equipa existe pelo id
        /// </summary>
        /// <param name="id">id da equipa</param>
        /// <returns>Retorna true/false</returns>
        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
