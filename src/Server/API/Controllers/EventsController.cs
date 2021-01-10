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
using DATA.Jwt;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class EventsController : ControllerBase
    {
        private readonly MyContext _context;

        public EventsController(MyContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Devolve todos os eventos
        /// </summary>
        /// <returns>Retorna uma lista com todos os eventos</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var aux = HttpContext.User;
            return await _context.Events.ToListAsync();
        }


        /// <summary>
        /// Devolve um evento pela id
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna um evento</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }


        /// <summary>
        /// Retorna as equipas presentes no evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna uma lista de equipas</returns>
        [HttpGet("GetTeamsByEventId/{id}")]
        [AllowAnonymous]
        public async Task<ICollection<Team>> GetTeamsByEventId(int id)
        {
            var teams = _context.Events.Include("Teams").FirstOrDefault(a => a.EventId == id).Teams;
            return teams;
        }


        /// <summary>
        /// Adiciona nova equipa ao evento
        /// </summary>
        /// <param name="idEvent">id do evento</param>
        /// <param name="idTeam">id da equipa</param>
        /// <returns>Retorna true/false</returns>
        [HttpPost("AddTeamToEvent")]
        [Authorize("Bearer")]
        public async Task<bool> AddTeamToEvent(int idEvent, int idTeam)
        {
            try
            {
                var tm = await _context.Teams.FindAsync(idTeam);
                _context.Events.Include("Teams").FirstOrDefault(x => x.EventId == idEvent).Teams.Add(tm);
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
        /// Remove uma equipa do evento
        /// </summary>
        /// <param name="idEvent">id do evento</param>
        /// <param name="idTeam">id da equipa</param>
        /// <returns>Retorna true/false</returns>
        [HttpPost("RemoveTeamFromEvent")]
        [Authorize("Bearer")]
        public async Task<bool> RemoveTeamFromEvent(int idEvent, int idTeam)
        {
            try
            {
                var tm = await _context.Teams.FindAsync(idTeam);
                if (tm != null)
                    NotFound();
                    
                _context.Events.Include("Teams").FirstOrDefault(x => x.EventId == idEvent).Teams.Remove(tm);
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
        /// Edita um evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <param name="event">objeto com os dados alterados</param>
        /// <returns>Retorna o resultado</returns>
        [HttpPut("{id}")]
        [Authorize("Bearer")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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
        /// Adiciona novo evento
        /// </summary>
        /// <param name="event">objeto do novo evento</param>
        /// <returns>Retorna o evento</returns>
        [HttpPost]
        [Authorize("Bearer")]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }


        /// <summary>
        /// Elimina um evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna o resultado</returns>
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Verifica se existe um evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>retorna retorna true/false</returns>
        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
