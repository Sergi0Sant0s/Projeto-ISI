using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository
{
    public class PlayersRepository
    {
        private TeamsRepository contextTeams;

        const string get_method = "Players";
        const string post_method = "Players";
        const string put_method = "Players";
        const string delete_method = "Players";

        public PlayersRepository()
        {
            contextTeams = new TeamsRepository();
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            List<Player> pl = null;
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
                                pl = JsonConvert.DeserializeObject<List<Player>>(await response.Content.ReadAsStringAsync());
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                        else
                        {
                            //ERROR
                            return pl;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return pl;
        }

        public async Task<Player> GetPlayerById(int? id)
        {
            Player pl = null;
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
                            pl = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync());
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

            return pl;
        }

        public async Task<Player> AddNewPlayer(HttpContext ctx, Player newPlayer)
        {
            Player pl = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newPlayer), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            pl = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync());
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
            return pl;
        }

        public async Task<bool> EditPlayer(HttpContext ctx, Player editPlayer, int? id)
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editPlayer), Encoding.UTF8, "application/json");
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

        public async Task<bool> DeletePlayer(HttpContext ctx, int? id)
        {
            Player deletePlayer = await GetPlayerById(id);
            if (deletePlayer != null)
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
