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
    public class StatPlayerOnMapsController : ControllerBase
    {
        private readonly MyContext _context;

        public StatPlayerOnMapsController(MyContext context)
        {
            _context = context;
        }

        // GET: StatPlayerOnMaps
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatPlayerOnMap>>> GetStatPlayerOnMap()
        {
            return await _context.StatPlayerOnMap.ToListAsync();
        }

        // GET: StatPlayerOnMaps/5
        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<StatPlayerOnMap>> GetStatPlayerOnMap(int id)
        {
            var statPlayerOnMap = await _context.StatPlayerOnMap.FindAsync(id);

            if (statPlayerOnMap == null)
            {
                return NotFound();
            }

            return statPlayerOnMap;
        }

        // PUT: StatPlayerOnMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatPlayerOnMap(int id, StatPlayerOnMap statPlayerOnMap)
        {
            if (id != statPlayerOnMap.StatPlayerOnMapId)
            {
                return BadRequest();
            }

            _context.Entry(statPlayerOnMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatPlayerOnMapExists(id))
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

        // POST: StatPlayerOnMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<StatPlayerOnMap>> PostStatPlayerOnMap(StatPlayerOnMap statPlayerOnMap)
        {
            try
            {
                _context.StatPlayerOnMap.Add(statPlayerOnMap);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetStatPlayerOnMap", new { id = statPlayerOnMap.StatPlayerOnMapId }, statPlayerOnMap);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        // DELETE: StatPlayerOnMaps/5
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatPlayerOnMap(int id)
        {
            var statPlayerOnMap = await _context.StatPlayerOnMap.FindAsync(id);
            if (statPlayerOnMap == null)
            {
                return NotFound();
            }

            _context.StatPlayerOnMap.Remove(statPlayerOnMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatPlayerOnMapExists(int id)
        {
            return _context.StatPlayerOnMap.Any(e => e.StatPlayerOnMapId == id);
        }
    }
}
