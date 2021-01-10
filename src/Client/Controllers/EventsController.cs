using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EventsController : Controller
    {
        //
        private EventsRepository contextEvents = null;
        private GamesRepository contextGames = null;
        private TeamsRepository contextTeams = null;

        public EventsController()
        {
            this.contextEvents = new EventsRepository();
            this.contextGames = new GamesRepository();
            this.contextTeams = new TeamsRepository();
        }

        /// <summary>
        /// Pagina inicial dos eventos
        /// </summary>
        /// <returns>Retorna view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var aux = await contextEvents.GetAllEvents();
                return aux == null ? RedirectToAction("Index", "Home") : View(aux);
            }
            catch (Exception e)
            {

                return View("Error", new Error { Title = "Listagem de competições", Message = e.Message });
            }

        }


        /// <summary>
        /// Pagina das equipas do evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Teams(int? id)
        {
            Event ev = null;
            IEnumerable<Team> tms = null;

            try
            {
                ev = await contextEvents.GetEventById(id);
                tms = await contextTeams.GetAllTeams(HttpContext);
                ViewBag.Event = ev;
                ViewBag.Teams = tms.Where(m => !ev.Teams.Any(a => a.TeamId == m.TeamId)).ToList();
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Listagem de jogos", Message = e.Message });
            }

            if (ev == null || tms == null)
            {
                return NotFound();
            }
            //
            return View("~/Views/Events/Teams.cshtml");
        }


        /// <summary>
        /// Pagina de detalhes do evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            Event ev = null;


            if (id == null)
            {
                return View(NotFound());
            }
            try
            {
                ev = await contextEvents.GetEventById(id);
                ev.Games = await contextGames.GetGameByEventId(id);
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Listagem da competição", Message = e.Message });
            }

            if (ev == null || ev.Games == null)
            {
                return View(NotFound());
            }

            return View(ev);
        }


        /// <summary>
        /// Adiciona nova equipa ao evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <param name="TeamId">id da equipa</param>
        /// <returns>Retorna a view das equipas</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddTeam([Bind("id,TeamId")] int? id, int? TeamId)
        {
            Event ev = null;
            Team tm = null;


            if (id == null && TeamId == null)
            {
                return RedirectToAction("Teams");
            }
            try
            {
                ev = await contextEvents.GetEventById(id);
                tm = await contextTeams.GetTeamById(TeamId);
                if (ev != null && tm != null)
                {
                    if (!await contextEvents.AddTeamToEvent(HttpContext, ev, tm))
                    {
                        return RedirectToAction("Teams", new { id = id });
                    }
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Adicionar equipa a competição", Message = e.Message });
            }

            if (ev == null || ev.Games == null)
            {
                return View(NotFound());
            }
            return RedirectToAction("Teams", new { id = id });
        }


        /// <summary>
        /// Remove equipa do evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <param name="TeamId">id da equipa</param>
        /// <returns>Retorna a view das equipas</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> RemoveTeam(int? id, int? TeamId)
        {
            Event ev = null;
            Team tm = null;


            if (id == null || TeamId == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                ev = await contextEvents.GetEventById(id);
                tm = await contextTeams.GetTeamById(TeamId);
                if (ev != null && tm != null)
                {
                    if (!await contextEvents.RemoveTeamFromEvent(HttpContext, ev, tm))
                    {
                        return RedirectToAction("Teams", new { id = id });
                    }
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Adicionar equipa a competição", Message = e.Message });
            }

            if (ev == null || ev.Games == null)
            {
                return View(NotFound());
            }
            return RedirectToAction("Teams", new { id = id });
        }


        /// <summary>
        /// Pagina de criar novo evento
        /// </summary>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Cria novo evento
        /// </summary>
        /// <param name="event">objeto evento</param>
        /// <returns>Retorna a view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([Bind("EventId,EventName,DateOfStart,DateOfEnd")] Event @event)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextEvents.AddNewEvent(HttpContext, @event);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Nova competição", Message = e.Message });
            }

            return View(@event);
        }


        /// <summary>
        /// Pagina que edita um evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a View</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            Event ev = null;
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                ev = await contextEvents.GetEventById(id);
            }
            catch (Exception e)
            {

                View("Error", new Error() { Title = "Alteração de dados", Message = e.Message });
            }

            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }


        /// <summary>
        /// Edita um evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <param name="event">objeto evento alterado</param>
        /// <returns>Retorna a View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,DateOfStart,DateOfEnd")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ev = await contextEvents.EditEvent(HttpContext, @event, id);

                    if (ev != null)
                    {
                        View("Details", ev);
                    }
                    else
                    {
                        View("Error", new Error() { Title = "Alteração de dados", Message = "Não foi possivel alterar os dados pretendidos" });
                    }
                }
                catch (Exception e)
                {
                    View("Error", new Error() { Title = "Alteração de dados", Message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }


        /// <summary>
        /// Pagina para eliminar um evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a View</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var ev = await contextEvents.GetEventById(id);
                if (ev == null)
                {
                    return NotFound();
                }

                return View(ev);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar evento", Message = e.Message });
            }
            
            
        }


        /// <summary>
        /// Elimina um evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a View</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await contextEvents.DeleteEvent(HttpContext, id))
                return RedirectToAction(nameof(Index));
            else
                return View("Error", new Error() { Title = "Eliminar evento", Message = "Não foi possivel eliminar o evento pretendido" });
        }
        

        /// <summary>
        /// Verifica se existem equipas para adicionar ao evento
        /// </summary>
        /// <param name="idEvent">id do evento</param>
        /// <returns>Retorna true/false</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<bool> VerifyTeams(int? idEvent)
        {
            var count = contextTeams.GetAllTeams(HttpContext).Result.Count;
            return count > 0 ? true : false;
        }


        /// <summary>
        /// Verifica se existe pelo menos 2 equipas
        /// </summary>
        /// <param name="idEvent">id do evento</param>
        /// <returns>Retorna true/false</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<bool> VerifyGames(int? idEvent)
        {
            var count = contextEvents.GetEventById(idEvent).Result.Teams.Count;
            return count > 1 ? true : false;
        }
    }
}
