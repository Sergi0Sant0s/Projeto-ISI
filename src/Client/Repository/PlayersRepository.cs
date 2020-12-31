using Client.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
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
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Program.Token.Token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        response = await client.GetAsync(get_method);

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                pl = JsonConvert.DeserializeObject<List<Player>>(await response.Content.ReadAsStringAsync());
                                foreach (var item in pl)
                                {
                                    if (item.TeamId != null)
                                        item.Team = await contextTeams.GetTeamById(item.TeamId);
                                }
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
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
            Player pl;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Program.Token.Token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        HttpResponseMessage response = await client.GetAsync(get_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Finded
                            pl = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync());
                            if (pl.TeamId != null)
                                pl.Team = await contextTeams.GetTeamById(pl.TeamId);
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

        public async Task<Player> AddNewPlayer(Player newPlayer)
        {
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
            Player pl;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Program.Token.Token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //POST
                        var player = new
                        {
                            Name = newPlayer.Name,
                            Nickname = newPlayer.Nickname,
                            Age = newPlayer.Age,
                            Nationality = newPlayer.Nationality,
                            Facebook = newPlayer.Facebook,
                            Twitter = newPlayer.Twitter,
                            Instagram = newPlayer.Instagram
                        };
                        var stringContent = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            pl = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync());
                            if (pl.TeamId != null)
                                pl.Team = await contextTeams.GetTeamById(pl.TeamId);
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

        public async Task<Player> EditPlayer(Player editPlayer, int? id)
        {
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
            Player pl;
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Program.Token.Token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //PUT
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editPlayer), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            pl = JsonConvert.DeserializeObject<Player>(await response.Content.ReadAsStringAsync());
                            if (pl.TeamId != null)
                                pl.Team = await contextTeams.GetTeamById(pl.TeamId);
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

        public async Task<bool> DeletePlayer(int? id)
        {
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return false;
            Player deletePlayer = await GetPlayerById(id);
            if (deletePlayer != null)
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri(Program.Url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Program.Token.Token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //DELETE
                        HttpResponseMessage response = await client.DeleteAsync(delete_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //DELETED
                            return true;
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
