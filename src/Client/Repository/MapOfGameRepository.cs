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
    public class MapOfGameRepository
    {
        GamesRepository games;

        const string get_method = "MapOfGames";
        const string getMapOfMapOfGamesByGame_method = "MapOfGames/MapOfGamesByGameId";
        const string post_method = "MapOfGames";
        const string put_method = "MapOfGames";
        const string delete_method = "MapOfGames";

        public MapOfGameRepository()
        {
            games = new GamesRepository();
        }

        public async Task<List<MapOfGame>> GetAllMapOfGames(HttpContext ctx)
        {
            List<MapOfGame> mog = null;
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
                                mog = JsonConvert.DeserializeObject<List<MapOfGame>>(await response.Content.ReadAsStringAsync());
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
                            return mog;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return mog;
        }

        public async Task<MapOfGame> GetMapOfGameById(HttpContext ctx, int? id)
        {
            MapOfGame mog = null;
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
                        //GET
                        HttpResponseMessage response = await client.GetAsync(get_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Finded
                            mog = JsonConvert.DeserializeObject<MapOfGame>(await response.Content.ReadAsStringAsync());
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

            return mog;
        }

        public async Task<List<MapOfGame>> GetMapOfGameByGameId(int? id)
        {
            List<MapOfGame> mog = null;
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
                        HttpResponseMessage response = await client.GetAsync(getMapOfMapOfGamesByGame_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Finded
                            mog = JsonConvert.DeserializeObject<List<MapOfGame>>(await response.Content.ReadAsStringAsync());
                            foreach (var item in mog)
                            {
                                if (item.GameId != null)
                                    item.Game = await new GamesRepository().GetGameById(item.GameId);
                                if (item.MapaId != null)
                                    item.Mapa = await new MapsRepository().GetMapById(item.MapaId);
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

            return mog;
        }

        public async Task<MapOfGame> AddNewMapOfGame(HttpContext ctx, MapOfGame newMapOfGame)
        {
            MapOfGame mog = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newMapOfGame), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            mog = JsonConvert.DeserializeObject<MapOfGame>(await response.Content.ReadAsStringAsync());
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
            return mog;
        }

        public async Task<bool> DeleteMapOfGame(HttpContext ctx, int? id)
        {
            MapOfGame deleteEvent = await GetMapOfGameById(ctx, id);
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
