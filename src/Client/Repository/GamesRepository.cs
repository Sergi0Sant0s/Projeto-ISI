using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository
{
    public class GamesRepository
    {
        TeamsRepository contextTeams;
        EventsRepository contextEvents;
        StatPlayerOnGameRepository contextStatPlayerOnGame = null;

        const string get_method = "Games";
        const string getGamesByEvent_method = "Games/GamesByEventId";
        const string post_method = "Games";
        const string put_method = "Games";
        const string delete_method = "Games";

        public GamesRepository()
        {
            contextTeams = new TeamsRepository();
            contextEvents = new EventsRepository();
            contextStatPlayerOnGame = new StatPlayerOnGameRepository();
        }

        public async Task<List<Game>> GetAllGames(HttpContext ctx)
        {
            List<Game> gm = null;
            HttpResponseMessage response = null;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        response = await client.GetAsync(get_method);

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                gm = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
                                foreach (Game item in gm)
                                    item.StatPlayerOnGame = contextStatPlayerOnGame.GetStatPlayerOnGameByGameId(item.GameId).Result;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            await ctx.SignOutAsync();
                            throw new InvalidOperationException("Falha de autenticação");
                        }
                        else
                        {
                            //ERROR
                            return gm;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return gm;
        }

        public async Task<Game> GetGameById(int? id)
        {
            Game gm = null;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        HttpResponseMessage response = await client.GetAsync(get_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Finded
                            gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
                            gm.Event = await contextEvents.GetEventById(gm.EventId);
                            gm.TeamA = await contextTeams.GetTeamById(gm.TeamAId);
                            gm.TeamB = await contextTeams.GetTeamById(gm.TeamBId);
                            if (gm.TeamWinnerId != null)
                                gm.TeamWinner = await contextTeams.GetTeamById(gm.TeamWinnerId);
                            gm.StatPlayerOnGame = await contextStatPlayerOnGame.GetStatPlayerOnGameByGameId(gm.GameId);
                        }
                        else
                        {
                            //ERROR
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return gm;
        }

        public async Task<List<Game>> GetGameByEventId(int? id)
        {
            List<Game> gm = null;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        HttpResponseMessage response = await client.GetAsync(getGamesByEvent_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Finded
                            gm = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
                            foreach (var item in gm)
                            {
                                item.Event = contextEvents.GetEventById(item.EventId).Result;
                                item.TeamA = contextTeams.GetTeamById(item.TeamAId).Result;
                                item.TeamB = contextTeams.GetTeamById(item.TeamBId).Result;
                                if (item.TeamWinnerId != null)
                                    item.TeamWinner = contextTeams.GetTeamById(item.TeamWinnerId).Result;
                                item.StatPlayerOnGame = contextStatPlayerOnGame.GetStatPlayerOnGameByGameId(item.GameId).Result;
                            }
                        }
                        else
                        {
                            //ERROR
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return gm;
        }

        public async Task<Game> AddNewGame(HttpContext ctx, Game newGame)
        {
            Game gm = null;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ctx.User.FindFirst(ClaimTypes.Rsa).Value);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //POST
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newGame), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
                            if(gm != null)
                            {
                                gm.Event = await contextEvents.GetEventById(gm.EventId);
                                gm.TeamA = await contextTeams.GetTeamById(gm.TeamAId);
                                gm.TeamB = await contextTeams.GetTeamById(gm.TeamBId);
                                if (gm.TeamWinnerId != null)
                                    gm.TeamWinner = await contextTeams.GetTeamById(gm.TeamWinnerId);
                            }
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            await ctx.SignOutAsync();
                            throw new InvalidOperationException("Falha de autenticação");
                        }
                        else
                        {
                            //ERROR
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return gm;
        }

        public async Task<bool> EditGame(HttpContext ctx, Game editGame, int? id)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ctx.User.FindFirst(ClaimTypes.Rsa).Value);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //PUT
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editGame), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            return true;
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            await ctx.SignOutAsync();
                            throw new InvalidOperationException("Falha de autenticação");
                            return false;
                        }
                        else
                        {
                            //ERROR
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteGame(HttpContext ctx, int? id)
        {
            Game deleteEvent = await GetGameById(id);
            if (deleteEvent != null)
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ctx.User.FindFirst(ClaimTypes.Rsa).Value);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //DELETE
                        HttpResponseMessage response = await client.DeleteAsync(delete_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //DELETED
                            return true;
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            await ctx.SignOutAsync();
                            throw new InvalidOperationException("Falha de autenticação");
                        }
                        else
                        {
                            //ERROR
                            return false;
                        }
                    }
                }
            return false;
        }
    }
}
