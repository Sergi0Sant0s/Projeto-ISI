using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class GamesController : Controller
    {
        GamesRepository contextGames = null;

        public GamesController(IConfiguration config)
        {
            contextGames = new GamesRepository(config);
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Maps(int? id)
        {
            return null;
        }


            public async Task<IActionResult> Details(int? id)
        {
            Game gm = null;


            if (id == null)
            {
                return View(NotFound());
            }
            try
            {
                gm = await contextGames.GetGameById(id);
                //gm.MapOfGames = await contextGames.GetGameByEventId(id);
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Listagem da competição", Message = e.Message });
            }

            if (gm == null || gm.MapOfGames == null)
            {
                return View(NotFound());
            }

            return View(gm);
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
