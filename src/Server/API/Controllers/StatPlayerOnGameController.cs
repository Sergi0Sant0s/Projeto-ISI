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
    public class StatPlayerOnGameController : ControllerBase
    {
        private readonly MyContext _context;

        public StatPlayerOnGameController(MyContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Devolve todas as estatisticas dos jogadores
        /// </summary>
        /// <returns>Retorna uma lista de objetos com as estatisticas dos jogadores</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatPlayerOnGame>>> GetStatPlayerOnGame()
        {
            return await _context.StatPlayerOnGame.ToListAsync();
        }


        /// <summary>
        /// Devolve uma estatistica pelo seu id
        /// </summary>
        /// <param name="id">id da estatistica</param>
        /// <returns>Devolve um objeto com a estatistica</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<StatPlayerOnGame>> GetStatPlayerOnGame(int id)
        {
            var StatPlayerOnGame = await _context.StatPlayerOnGame.FindAsync(id);

            if (StatPlayerOnGame == null)
            {
                return NotFound();
            }

            return StatPlayerOnGame;
        }


        /// <summary>
        /// Edita uma estatistica
        /// </summary>
        /// <param name="id">id da estatistica</param>
        /// <param name="StatPlayerOnGame">objeto com a estatistica alterada</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatPlayerOnGame(int id, StatPlayerOnGame StatPlayerOnGame)
        {
            if (id != StatPlayerOnGame.StatPlayerOnGameId)
            {
                return BadRequest();
            }

            _context.Entry(StatPlayerOnGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatPlayerOnGameExists(id))
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
        /// Adiciona nova estatistica
        /// </summary>
        /// <param name="StatPlayerOnGame">Objeto com a nova estatistica</param>
        /// <returns>Retorna um objeto com a nova estatistica</returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<StatPlayerOnGame>> PostStatPlayerOnGame(StatPlayerOnGame StatPlayerOnGame)
        {
            try
            {
                _context.StatPlayerOnGame.Add(StatPlayerOnGame);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetStatPlayerOnGame", new { id = StatPlayerOnGame.StatPlayerOnGameId }, StatPlayerOnGame);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }


        /// <summary>
        /// Elimina a estatistica pelo id
        /// </summary>
        /// <param name="id">id da estatistica</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatPlayerOnGame(int id)
        {
            var StatPlayerOnGame = await _context.StatPlayerOnGame.FindAsync(id);
            if (StatPlayerOnGame == null)
            {
                return NotFound();
            }

            _context.StatPlayerOnGame.Remove(StatPlayerOnGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Verifica se a estatistica existe
        /// </summary>
        /// <param name="id">id da estatistica</param>
        /// <returns>Retorna true/false</returns>
        private bool StatPlayerOnGameExists(int id)
        {
            return _context.StatPlayerOnGame.Any(e => e.StatPlayerOnGameId == id);
        }
    }
}
