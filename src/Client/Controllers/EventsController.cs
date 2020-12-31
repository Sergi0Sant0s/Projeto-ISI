﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
using Client.Repository;
using Microsoft.Extensions.Configuration;
using Client.ViewModel;
using Microsoft.Extensions.Logging;

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

        // GET: Events
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

        public async Task<IActionResult> Teams(int? id)
        {
            Event ev = null;
            IEnumerable<Team> tms = null;

            try
            {
                ev = await contextEvents.GetEventById(id);
                tms = await contextTeams.GetAllTeams();
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
            return View("~/Views/TeamsEvent/Index.cshtml");
        }

        // GET: Events/Details/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam([Bind("id,TeamId")] int? id, int? TeamId)
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");

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
                    if (!await contextEvents.AddTeamToEvent(ev, tm))
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

        public async Task<IActionResult> RemoveTeam(int? id, int? TeamId)
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");

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
                    if (!await contextEvents.RemoveTeamFromEvent(ev, tm))
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

        // GET: Events/Create
        public IActionResult Create()
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,DateOfStart,DateOfEnd")] Event @event)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextEvents.AddNewEvent(@event);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Nova competição", Message = e.Message });
            }

            return View(@event);
        }

        // GET: Events/Edit/5
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

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    var ev = await contextEvents.EditEvent(@event, id);

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

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ev = await contextEvents.GetEventById(id);
            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await contextEvents.DeleteEvent(id))
                return RedirectToAction(nameof(Index));
            else
                return View("Error", new Error() { Title = "Eliminar evento", Message = "Não foi possivel eliminar o evento pretendido" });
        }
        
        [HttpPost]
        public async Task<bool> VerifyTeams(int? idEvent)
        {
            var count = contextTeams.GetAllTeams().Result.Count;
            return count > 0 ? true : false;
        }

        [HttpPost]
        public async Task<bool> VerifyGames(int? idEvent)
        {
            var count = contextEvents.GetEventById(idEvent).Result.Teams.Count;
            return count > 1 ? true : false;
        }
    }
}
