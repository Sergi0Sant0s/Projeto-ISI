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
    public class MapsController : ControllerBase
    {
        private readonly MyContext _context;

        public MapsController(MyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve os mapas
        /// </summary>
        /// <returns>Devolve uma lista de mapas</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Map>>> GetMaps()
        {
            return await _context.Maps.ToListAsync();
        }

        /// <summary>
        /// Devolve mapa pelo id
        /// </summary>
        /// <param name="id">id do mapa</param>
        /// <returns>Retorna um mapa</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Map>> GetMap(int id)
        {
            var map = await _context.Maps.FindAsync(id);

            if (map == null)
            {
                return NotFound();
            }

            return map;
        }


        /// <summary>
        /// Edita um mapa
        /// </summary>
        /// <param name="id">id do mapa</param>
        /// <param name="map">objeto com o mapa alterado</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMap(int id, Map map)
        {
            if (id != map.MapId)
            {
                return BadRequest();
            }

            _context.Entry(map).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapExists(id))
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
        /// Adiciona novo mapa
        /// </summary>
        /// <param name="map">objeto com o novo mapa</param>
        /// <returns>Retorna o mapa novo</returns>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<Map>> PostMap(Map map)
        {
            _context.Maps.Add(map);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMap", new { id = map.MapId }, map);
        }


        /// <summary>
        /// Elimina um mapa pelo id
        /// </summary>
        /// <param name="id">id do mapa</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMap(int id)
        {
            var map = await _context.Maps.FindAsync(id);
            if (map == null)
            {
                return NotFound();
            }

            _context.Maps.Remove(map);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Verifica se existe
        /// </summary>
        /// <param name="id">id do mapa</param>
        /// <returns>Retorna true/false</returns>
        private bool MapExists(int id)
        {
            return _context.Maps.Any(e => e.MapId == id);
        }
    }
}
