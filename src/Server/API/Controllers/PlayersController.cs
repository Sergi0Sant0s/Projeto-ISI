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

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly MyContext _context;

        public PlayersController(MyContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Devolve todos os jogadores
        /// </summary>
        /// <returns>Retorna uma lista de jogadores</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }


        /// <summary>
        /// Devolve um jogador pelo seu id
        /// </summary>
        /// <param name="id">id do jogador</param>
        /// <returns>Retorna um objeto do tipo jogador</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }


        /// <summary>
        /// Edita um jogador
        /// </summary>
        /// <param name="id">id do jogador</param>
        /// <param name="player">objeto com o jogador alterado</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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
        /// Adiciona novo jogador
        /// </summary>
        /// <param name="player">Objeto com o novo jogador</param>
        /// <returns>Retorna um objeto com o novo jogador</returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            try
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
            return CreatedAtAction("GetPlayer", new { id = player.PlayerId }, player);
        }


        /// <summary>
        /// Elimina o jogador pelo seu id
        /// </summary>
        /// <param name="id">id do jogador</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Verifica se o jogador existe
        /// </summary>
        /// <param name="id">id do jogador</param>
        /// <returns>Retorna true/false</returns>
        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
