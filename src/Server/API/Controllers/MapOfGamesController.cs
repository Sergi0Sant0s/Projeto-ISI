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
    public class MapOfGamesController : ControllerBase
    {
        private readonly MyContext _context;

        public MapOfGamesController(MyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve os maps jogados em um jogo
        /// </summary>
        /// <returns>Retorna uma lista de mapas jogados</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapOfGame>>> GetMapOfGame()
        {
            return await _context.MapOfGame.ToListAsync();
        }


        /// <summary>
        /// Devolve o map jogado pelo id
        /// </summary>
        /// <param name="id">id do mapa jogado</param>
        /// <returns>Retorna um mapa jogado</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<MapOfGame>> GetMapOfGame(int id)
        {
            var mapOfGame = await _context.MapOfGame.FindAsync(id);

            if (mapOfGame == null)
            {
                return NotFound();
            }

            return mapOfGame;
        }


        /// <summary>
        /// Devolve uma lista de mapas jogados pelo id do jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <returns>Retorna uma lista de mapas jogados</returns>
        [AllowAnonymous]
        [HttpGet("MapOfGamesByGameId/{id}")]
        public async Task<ActionResult<List<MapOfGame>>> MapOfGamesByGameId(int id)
        {
            return await _context.MapOfGame.Where(b => b.GameId == id).ToListAsync();
        }

        /// <summary>
        /// Edita um mapa jogado
        /// </summary>
        /// <param name="id">id do mapa jogado</param>
        /// <param name="mapOfGame">objeto com o mapa jogado alterado</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapOfGame(int id, MapOfGame mapOfGame)
        {
            if (id != mapOfGame.MapOfGameId)
            {
                return BadRequest();
            }

            _context.Entry(mapOfGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapOfGameExists(id))
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
        /// Adiciona novo mapa jogado
        /// </summary>
        /// <param name="mapOfGame">objeto com o novo mapa jogado</param>
        /// <returns>Retorna o novo mapa jogado</returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<MapOfGame>> PostMapOfGame(MapOfGame mapOfGame)
        {
            _context.MapOfGame.Add(mapOfGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMapOfGame", new { id = mapOfGame.MapOfGameId }, mapOfGame);
        }


        /// <summary>
        /// Elimina um mapa jogado pelo id
        /// </summary>
        /// <param name="id">id do mapa jogado</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapOfGame(int id)
        {
            var mapOfGame = await _context.MapOfGame.FindAsync(id);
            if (mapOfGame == null)
            {
                return NotFound();
            }

            _context.MapOfGame.Remove(mapOfGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se existe um mapa jogado
        /// </summary>
        /// <param name="id">id do mapa jogado</param>
        /// <returns>Retorna true/false</returns>
        private bool MapOfGameExists(int id)
        {
            return _context.MapOfGame.Any(e => e.MapOfGameId == id);
        }
    }
}
