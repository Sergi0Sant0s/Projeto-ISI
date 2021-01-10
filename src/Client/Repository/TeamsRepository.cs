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
    public class TeamsRepository
    {
        const string get_method = "Teams";
        const string post_method = "Teams";
        const string AddPlayerToTeam_method = "Teams/AddPlayerToTeam/";
        const string RemovePlayerFromTeam_method = "Teams/RemovePlayerFromTeam/";
        const string put_method = "Teams";
        const string delete_method = "Teams";



        public async Task<List<Team>> GetAllTeams(HttpContext ctx)
        {
            List<Team> tm = null;
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
                                tm = JsonConvert.DeserializeObject<List<Team>>(await response.Content.ReadAsStringAsync());
                                foreach (var item in tm)
                                    item.Players = new PlayersRepository().GetAllPlayers().Result.Where(x => x.TeamId == item.TeamId).ToList();
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
                            return tm;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tm;
        }

        public async Task<Team> GetTeamById(int? id)
        {
            Team tm = null;
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
                            tm = JsonConvert.DeserializeObject<Team>(await response.Content.ReadAsStringAsync());
                            tm.Players = new PlayersRepository().GetAllPlayers().Result.Where(x => x.TeamId == tm.TeamId).ToList();
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

            return tm;
        }

        public async Task<Team> AddNewTeam(HttpContext ctx, Team newTeam)
        {
            Team tm = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newTeam), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            tm = JsonConvert.DeserializeObject<Team>(await response.Content.ReadAsStringAsync());
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
            return tm;
        }

        public async Task<bool> AddPlayerToTeam(HttpContext ctx, Team tm, Player pl)
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
                        //POST
                        HttpResponseMessage response = await client.PostAsync(AddPlayerToTeam_method + $"?idTeam={tm.TeamId}&idPlayer={pl.PlayerId}", new StringContent(string.Empty));

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
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

        public async Task<bool> RemovePlayerFromTeam(HttpContext ctx, Team tm, Player pl)
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
                        //POST
                        HttpResponseMessage response = await client.PostAsync(RemovePlayerFromTeam_method + $"?idTeam={tm.TeamId}&idPlayer={pl.PlayerId}", new StringContent(string.Empty));

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
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

        public async Task<Team> EditTeam(HttpContext ctx, Team editTeam, int? id)
        {
            Team tm = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editTeam), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            tm = JsonConvert.DeserializeObject<Team>(await response.Content.ReadAsStringAsync());
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
            return tm;
        }

        public async Task<bool> DeleteTeam(HttpContext ctx, int? id)
        {
            Team deleteTeam = await GetTeamById(id);
            if (deleteTeam != null)
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
