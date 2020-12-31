using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
using Client.Repository;
using Microsoft.Extensions.Configuration;

namespace Client.Controllers
{
    public class PlayersController : Controller
    {
        private readonly PlayersRepository contextPlayers;

        public PlayersController()
        {
            contextPlayers = new PlayersRepository();
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");
            return View(await contextPlayers.GetAllPlayers());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Player = await contextPlayers.GetPlayerById(id);
            if (Player == null)
            {
                return NotFound();
            }

            return View(Player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,Name,Nickname,Age,Nationality,Facebook,Twitter,Instagram")] Player player)
        {
            if (ModelState.IsValid)
            {
                player.StatPlayerOnMaps = null;
                await contextPlayers.AddNewPlayer(player);
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Player = await contextPlayers.GetPlayerById(id);
            if (Player == null)
            {
                return NotFound();
            }
            return View(Player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,Name,Nickname,Age,Nationality,Facebook,Twitter,Instagram")] Player Player)
        {
            if (id != Player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await contextPlayers.EditPlayer(Player, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(Player.PlayerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Player = await contextPlayers.GetPlayerById(id);
            if (Player == null)
            {
                return NotFound();
            }

            return View(Player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Player = await contextPlayers.DeletePlayer(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return contextPlayers.GetPlayerById(id) != null;
        }
    }
}
