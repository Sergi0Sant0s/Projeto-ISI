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

        // GET: MapOfGames
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapOfGame>>> GetMapOfGame()
        {
            return await _context.MapOfGame.ToListAsync();
        }

        // GET: MapOfGames/5
        [Authorize("Bearer")]
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

        // GET: MapOfGames/MapOfGamesByGameId/5
        [Authorize("Bearer")]
        [HttpGet("MapOfGamesByGameId/{id}")]
        public async Task<ActionResult<List<MapOfGame>>> MapOfGamesByGameId(int id)
        {
            return await _context.MapOfGame.Where(b => b.GameId == id).ToListAsync();

        }

        // PUT: MapOfGames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: MapOfGames
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<MapOfGame>> PostMapOfGame(MapOfGame mapOfGame)
        {
            _context.MapOfGame.Add(mapOfGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMapOfGame", new { id = mapOfGame.MapOfGameId }, mapOfGame);
        }

        // DELETE: MapOfGames/5
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

        private bool MapOfGameExists(int id)
        {
            return _context.MapOfGame.Any(e => e.MapOfGameId == id);
        }
    }
}
