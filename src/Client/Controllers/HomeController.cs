using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Client.Repository;

namespace Client.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Events");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RouteError()
        {
            return RedirectToAction("Events", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { Title = "Autorização", Message = "Utilizador ou password invalidos"});
        }

        [AllowAnonymous]
        public async Task<IActionResult> Events()
        {
            ViewBag.Events = await new EventsRepository().GetAllEvents();
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Games(int? id)
        {
            if (id != null)
            {
                ViewBag.Event = await new EventsRepository().GetEventById(id);
                ViewBag.Games = await new GamesRepository().GetGameByEventId(id);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Players()
        {
            ViewBag.Players = await new PlayersRepository().GetAllPlayers();
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Teams()
        {
            ViewBag.Teams = await new TeamsRepository().GetAllTeams(HttpContext);
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> TeamsEvent(int? id)
        {
            ViewBag.Event = await new EventsRepository().GetEventById(id);
            ViewBag.Teams = await new EventsRepository().GetTeamsByEventId(id);
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Stats(int? id)
        {
            ViewBag.Game = await new GamesRepository().GetGameById(id);
            ViewBag.Stats = await new StatPlayerOnGameRepository().GetStatPlayerOnGameByGameId(id);
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> MapOfGame(int? id)
        {
            ViewBag.Game = await new GamesRepository().GetGameById(id);
            ViewBag.MapsOfGame = await new MapOfGameRepository().GetMapOfGameByGameId(id);
            return View();
        }
    }
}
