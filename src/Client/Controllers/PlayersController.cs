using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await contextPlayers.GetAllPlayers());
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Listagem de jogadores", Message = e.Message });
            }
            
        }

        // GET: Players/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Detalhes de jogadores", Message = e.Message });
            }
            
        }

        // GET: Players/Create
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([Bind("PlayerId,Name,Nickname,Age,Nationality,Facebook,Twitter,Instagram")] Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    player.StatPlayerOnMaps = null;
                    await contextPlayers.AddNewPlayer(HttpContext, player);
                    return RedirectToAction(nameof(Index));
                }
                return View(player);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Detalhes de jogadores", Message = e.Message });
            }
        }

        // GET: Players/Edit/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Editar de jogador", Message = e.Message });
            }
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
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
                    await contextPlayers.EditPlayer(HttpContext,Player, id);
                }
                catch (Exception e)
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar jogador", Message = e.Message });
            }            
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var Player = await contextPlayers.DeletePlayer(HttpContext, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar jogador", Message = e.Message });
            }

            
        }

        [Authorize(Roles = "Admin,User")]
        private bool PlayerExists(int id)
        {
            return contextPlayers.GetPlayerById(id) != null;
        }
    }
}
