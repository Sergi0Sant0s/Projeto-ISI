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
    public class GamesController : ControllerBase
    {
        private readonly MyContext _context;

        public GamesController(MyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve todos os jogos
        /// </summary>
        /// <returns>Retorna uma lista de jogos</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }


        /// <summary>
        /// Devolve um jogo pela id
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <returns>Retorna um jogo</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }


        /// <summary>
        /// Devolve todos os jogos de um determinado evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna uma lista de jogos</returns>
        [AllowAnonymous]
        [HttpGet("GamesByEventId/{id}")]
        public async Task<ActionResult<List<Game>>> GamesByEventId(int id)
        {
            return await _context.Games.Where(b => b.EventId == id).ToListAsync();

        }


        /// <summary>
        /// Devolve as estatisticas dos jogadores de um determinado evento
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <returns>Retorna uma lista de estatisticas de jogadores</returns>
        [AllowAnonymous]
        [HttpGet("StatPlayerByGameId/{id}")]
        public async Task<ActionResult<List<StatPlayerOnGame>>> StatPlayerByGameId(int id)
        {
            return await _context.StatPlayerOnGame.Where(b => b.GameId == id).ToListAsync();
        }


        /// <summary>
        /// Edita um jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <param name="game">objeto com o jogo alterado</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.GameId)
            {
                return BadRequest();
            }

            if (game.TeamWinnerId == 0)
                game.TeamWinnerId = null;

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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
        /// Adiciona novo jogo
        /// </summary>
        /// <param name="game">objeto com o novo jogo</param>
        /// <returns>Retorna o novo jogo</returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            try
            {
                game.TeamA = _context.Teams.Find(game.TeamAId);
                game.TeamB = _context.Teams.Find(game.TeamBId);
                game.TeamWinner = _context.Teams.Find(game.TeamWinnerId);
                game.Event = _context.Events.Find(game.EventId);
                if(game.TeamWinnerId == 0)
                    game.TeamWinnerId = null;
                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetGame", new { id = game.GameId }, game);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Elimina um jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Verifica se existe um jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <returns>Retorna true/false</returns>
        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}
