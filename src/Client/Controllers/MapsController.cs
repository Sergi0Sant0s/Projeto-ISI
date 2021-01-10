using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class MapsController : Controller
    {
        private readonly MapsRepository contextMaps;

        public MapsController()
        {
            contextMaps = new MapsRepository();
        }

        // GET: Maps
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            var aux = await contextMaps.GetAllMaps(HttpContext);

            try
            {
                if (aux != null)
                    return View(aux);
                else
                    return View("Error", new Error() { Title = "Listagem de mapas", Message = "Não foi possivel apresentar os mapas pretendidos" });
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        // GET: Maps/Details/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Detalhes do mapa", Message = e.Message });
            }
            
        }

        // GET: Maps/Create
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([Bind("MapId,Description")] Map map)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextMaps.AddNewMap(HttpContext, map);
                    return RedirectToAction(nameof(Index));
                }
                return View(map);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Criar mapa", Message = e.Message });
            }
            
        }

        // GET: Maps/Edit/5
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Editar mapa", Message = e.Message });
            }
            
        }

        // POST: Maps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int id, [Bind("MapId,Description")] Map map)
        {
            if (map == null || id != map.MapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await contextMaps.EditMap(HttpContext, map,id);
                }
                catch (Exception)
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
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
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar mapa", Message = e.Message });
            }

            
        }

        // POST: Maps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var map = await contextMaps.GetMapById(id);
                await contextMaps.DeleteMap(HttpContext, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar mapa", Message = e.Message });
            }
            
        }

        [Authorize(Roles = "Admin,User")]
        private bool MapExists(int id)
        {
            try
            {
                return contextMaps.GetMapById(id) != null;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
