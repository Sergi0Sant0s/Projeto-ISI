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
    public class MapsController : Controller
    {
        private readonly MapsRepository contextMaps;

        public MapsController(IConfiguration config)
        {
            contextMaps = new MapsRepository(config);
        }

        // GET: Maps
        public async Task<IActionResult> Index()
        {
            return View(await contextMaps.GetAllMaps());
        }

        // GET: Maps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var map = await contextMaps.GetMapById(id);
            if (map == null)
            {
                return NotFound();
            }

            return View(map);
        }

        // GET: Maps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MapId,Description")] Map map)
        {
            if (ModelState.IsValid)
            {
                await contextMaps.AddNewMap(map);
                return RedirectToAction(nameof(Index));
            }
            return View(map);
        }

        // GET: Maps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var map = await contextMaps.GetMapById(id);
            if (map == null)
            {
                return NotFound();
            }
            return View(map);
        }

        // POST: Maps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MapId,Description")] Map map)
        {
            if (id != map.MapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await contextMaps.EditMap(map,id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MapExists(map.MapId))
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
            return View(map);
        }

        // GET: Maps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var map = await contextMaps.GetMapById(id);
            if (map == null)
            {
                return NotFound();
            }

            return View(map);
        }

        // POST: Maps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var map = await contextMaps.GetMapById(id);
            await contextMaps.DeleteMap(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MapExists(int id)
        {
            return contextMaps.GetMapById(id) != null;
        }
    }
}
