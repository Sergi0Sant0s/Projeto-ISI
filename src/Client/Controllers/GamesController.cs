using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        EventsRepository contextEvents = null;
        StatPlayerOnGameRepository contextStatPlayerOnGame = null;
        MapOfGameRepository contextMapOfGame = null;

        public GamesController()
        {
            contextGames = new GamesRepository();
            contextEvents = new EventsRepository();
            contextStatPlayerOnGame = new StatPlayerOnGameRepository();
            contextMapOfGame = new MapOfGameRepository();
        }


        /// <summary>
        /// Pagina inicial dos jogos
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                ViewBag.Event = await contextEvents.GetEventById(id);
                ViewBag.Games = await contextGames.GetGameByEventId(id);
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Listagem de jogos", Message = e.Message });
            }

            if (ViewBag.Event == null || ViewBag.Games == null)
            {
                return NotFound();
            }
            //
            return View();
        }


        /// <summary>
        /// Pagina para criar novo jogo
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(int? id)
        {
            //
            var ev = contextEvents.GetEventById(id);
            List<Team> teamA = ev.Result.Teams.AsEnumerable().ToList();
            List<Team> teamB = ev.Result.Teams.AsEnumerable().ToList();
            teamA.RemoveAt(1);
            teamB.RemoveAt(0);
            ViewBag.Event = ev.Result;
            ViewBag.TeamsA = new SelectList(teamA, nameof(Team.TeamId), nameof(Team.TeamName));
            ViewBag.TeamsB = new SelectList(teamB, nameof(Team.TeamId), nameof(Team.TeamName));
            
            return View();
        }


        /// <summary>
        /// Adiciona novo jogo ao evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <param name="game">objeto do novo jogo</param>
        /// <returns>Retorna a view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create(int? id, [Bind("EventId,GameId,GameDate,TeamAId,TeamBId,TeamWinnerId")] Game @game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    game.EventId = (int)id;
                    Game gm = await contextGames.AddNewGame(HttpContext, @game);
                    if(gm != null)
                    {
                        foreach (var item in gm.TeamA.Players)
                        {
                            StatPlayerOnGame stg = new StatPlayerOnGame() { Player = item, PlayerId = item.PlayerId, Adr = 0, Deaths = 0, Game = gm, GameId = gm.GameId, Kills = 0, Rating = 0, Team = gm.TeamA, TeamId = gm.TeamA.TeamId };
                            await contextStatPlayerOnGame.AddNewStatPlayerOnGame(HttpContext, stg);
                        }

                        foreach (var item in gm.TeamB.Players)
                        {
                            StatPlayerOnGame stg = new StatPlayerOnGame() { Player = item, PlayerId = item.PlayerId, Adr = 0, Deaths = 0, Rating = 0, Game = gm, GameId = gm.GameId, Kills = 0, Team = gm.TeamB, TeamId = gm.TeamB.TeamId };
                            await contextStatPlayerOnGame.AddNewStatPlayerOnGame(HttpContext, stg);
                        }
                    }

                    return RedirectToAction("Index", new { id = game.EventId });
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Novo Jogo", Message = e.Message });
            }

            return View(@game);
        }


        /// <summary>
        /// Pagina para a gestão dos mapas do jogo
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Maps(int? id)
        {
            return null;
        }


        /// <summary>
        /// Pagina para a gestão das estatisticas
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Stats(int? id)
        {
            Game gm;
            try
            {
                if ((gm = await contextGames.GetGameById(id)) != null)
                {
                    ViewBag.Game = gm;
                    return View();
                }
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Estatisticas", Message = e.Message });
            }
            

            return new EmptyResult();
        }


        /// <summary>
        /// Adiciona novo mapa ao jogo
        /// </summary>
        /// <param name="mapOfGame">objeto com o mapa</param>
        /// <returns>Retorna o resultado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AddMapOfGame([Bind("MapaId, GameId, TeamAresult, TeamBresult")] MapOfGame mapOfGame)
        {
            MapOfGame map = null;
            try
            {
                if (mapOfGame != null)
                {
                    if((map = await contextMapOfGame.AddNewMapOfGame(HttpContext, mapOfGame)) != null)
                    {
                        return RedirectToAction("MapOfGame", new { id = mapOfGame.GameId });
                    }
                }
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Mapas jogados", Message = e.Message });
            }
            return View("~/Views/Home/Events.cshtml");
        }


        /// <summary>
        /// Remove mapa do jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <param name="idMapOfGame">id do mapa</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> RemoveMapOfGame(int? id, int? idMapOfGame)
        {
            try
            {
                if(id != null && idMapOfGame != null)
                    if(await contextMapOfGame.DeleteMapOfGame(HttpContext, idMapOfGame))
                        return RedirectToAction("MapOfGame", new { id = id });
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Remover mapa jogado", Message = e.Message });
            }
            return View("~/Views/Home/Events.cshtml");
        }


        /// <summary>
        /// Abre a gestão de mapas do jogo
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> MapOfGame(int? id)
        {
            var mapOfGame = await contextMapOfGame.GetMapOfGameByGameId(id);
            var maps = await new MapsRepository().GetAllMaps(HttpContext);
            var mapsOutOfGame = maps.Where(l2 => !mapOfGame.Any(l1 => l1.MapaId == l2.MapId)).ToList();
            ViewBag.GameId = id;
            ViewBag.EventId = (await contextGames.GetGameById(id)).EventId;
            ViewBag.Maps = new SelectList(mapsOutOfGame, nameof(Map.MapId), nameof(Map.Description));
            ViewBag.MapsOfGame = mapOfGame;

            return View();
        }


        /// <summary>
        /// Edita as estatisticas do jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <param name="stat">objeto das estatisticas do jogo</param>
        /// <returns>Retorna true/false</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<bool> EditStat(int? id, [Bind("StatPlayerOnGameId,GameId,TeamId,PlayerId,Kills,Deaths,Adr,Rating")] StatPlayerOnGame stat)
        {
            try
            {
                if (stat != null && stat.StatPlayerOnGameId != null)
                {
                    return await contextStatPlayerOnGame.EditStatPlayerOnGame(HttpContext, stat, stat.StatPlayerOnGameId);
                }
                return false;               
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Devolve as equipas que podem ser escolhidas
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <param name="team">id da equipa ocupada</param>
        /// <returns>Retorna as equipas</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IEnumerable<object>> GetTeamsExcluded([Bind("id,team")]int? id, int? team)
        {
            try
            {
                if (id == null || team == null || team == 0)
                    return null;

                var auxTeams = contextEvents.GetEventById(id).Result.Teams;
                var teams = from t in auxTeams
                            where t.TeamId != team
                            select new { Value = t.TeamId, Text = t.TeamName };

                return teams;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Edita um jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <param name="game">objeto com os dados alterados</param>
        /// <returns>Retorna a view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit([Bind("EventId,GameId,GameDate,TeamAId,TeamBId,TeamWinnerId")] int? id, Game @game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextGames.EditGame(HttpContext, @game, id);
                    return RedirectToAction("Index", new { id = game.EventId });
                }
            }
            catch (Exception e)
            {
                View("Error", new Error() { Title = "Editar Jogo", Message = e.Message });
            }
            return RedirectToAction("Index","Events");
        }


        /// <summary>
        /// Pagina para editar um jogo
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                    return new EmptyResult();
                //
                var gm = await contextGames.GetGameById(id);
                var ev = gm.Event;
                List<Team> teamA = new List<Team>() { ev.Teams.AsEnumerable().FirstOrDefault(x => x.TeamId == gm.TeamAId) };
                List<Team> teamB = new List<Team>() { ev.Teams.AsEnumerable().FirstOrDefault(x => x.TeamId == gm.TeamBId) };
                List<SelectListItem> teamWinner = new List<SelectListItem>();


                teamWinner.Add(new SelectListItem() { Value = gm.TeamAId.ToString(), Text = gm.TeamA.TeamName });
                teamWinner.Add(new SelectListItem() { Value = gm.TeamBId.ToString(), Text = gm.TeamB.TeamName });
                if (gm.TeamWinnerId != null)
                    teamWinner.Where(x => x.Value == gm.TeamWinnerId.ToString() ? x.Selected = true : x.Selected = false);
                //Without Winner
                teamWinner.Insert(0, new SelectListItem() { Value = "0", Text = "Sem Vencedor" });

                ViewBag.Event = ev;
                ViewBag.TeamsA = new SelectList(teamA, nameof(Team.TeamId), nameof(Team.TeamName));
                ViewBag.TeamsB = new SelectList(teamB, nameof(Team.TeamId), nameof(Team.TeamName));
                ViewBag.TeamsWinner = new SelectList(teamWinner, "Value", "Text");
                //
                if (gm.TeamWinnerId == null)
                    gm.TeamWinnerId = 0;

                return View(gm);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Editar Jogo", Message = e.Message });
            }
            
        }


        /// <summary>
        /// Pagina para eliminar um jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <returns>Retorna a view</returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                Game gm = await contextGames.GetGameById(id);
                return View(gm);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar Jogo", Message = e.Message });
            }
            
        }


        /// <summary>
        /// Elimina um jogo
        /// </summary>
        /// <param name="id">id do jogo</param>
        /// <returns>Retorna a view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                int ev = contextGames.GetGameById(id).Result.EventId;
                if (await contextGames.DeleteGame(HttpContext, id))
                    return RedirectToAction("Index", new { id = ev });
                return RedirectToAction("Index", new { id = ev });
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar Jogo", Message = e.Message });
            }
           
        }
    }
}
