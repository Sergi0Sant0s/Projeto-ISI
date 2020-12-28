using Client.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository
{
    public class GamesRepository
    {
        private readonly IConfiguration _config;
        TeamsRepository teams;

        const string get_method = "Games";
        const string getGamesByEvent_method = "Games/GamesByEventId";
        const string post_method = "Games";
        const string put_method = "Games";
        const string delete_method = "Games";

        public GamesRepository(IConfiguration config)
        {
            this._config = config;
            teams = new TeamsRepository(_config);
        }

        public async Task<List<Game>> GetAllGames()
        {
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
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
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Program.Token.Token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        response = await client.GetAsync(get_method);

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                gm = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
                                foreach (var aux in gm)
                                {
                                    aux.TeamA = teams.GetTeamById(aux.TeamAid).Result;
                                    aux.TeamB = teams.GetTeamById(aux.TeamBid).Result;
                                    aux.TeamWinner = teams.GetTeamById(aux.TeamWinnerId).Result;
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
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
            Game gm;
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
                            gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
                            gm.TeamA = teams.GetTeamById(gm.TeamAid).Result;
                            gm.TeamB = teams.GetTeamById(gm.TeamBid).Result;
                            gm.TeamWinner = teams.GetTeamById(gm.TeamWinnerId).Result;
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
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
            List<Game> gm;
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
                        HttpResponseMessage response = await client.GetAsync(getGamesByEvent_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Finded
                            gm = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
                            foreach(var aux in gm)
                            {
                                aux.TeamA = teams.GetTeamById(aux.TeamAid).Result;
                                aux.TeamB = teams.GetTeamById(aux.TeamBid).Result;
                                if(aux.TeamWinnerId != null)
                                    aux.TeamWinner = teams.GetTeamById(aux.TeamWinnerId).Result;
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

        public async Task<Game> AddNewGame(Event newEvent)
        {
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
            Game gm;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newEvent), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
                            gm.TeamA = teams.GetTeamById(gm.TeamAid).Result;
                            gm.TeamB = teams.GetTeamById(gm.TeamBid).Result;
                            gm.TeamWinner = teams.GetTeamById(gm.TeamWinnerId).Result;
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

        public async Task<Game> EditGame(Event editEvent, int? id)
        {

            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return null;
            Game gm;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editEvent), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
                            gm.TeamA = teams.GetTeamById(gm.TeamAid).Result;
                            gm.TeamB = teams.GetTeamById(gm.TeamBid).Result;
                            gm.TeamWinner = teams.GetTeamById(gm.TeamWinnerId).Result;
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

        public async Task<bool> DeleteGame(int? id)
        {
            if (Program.Authentication == null || LoginRepository.Authenticate() == null || Program.Token == null)
                return false;
            Game deleteEvent = await GetGameById(id);
            if (deleteEvent != null)
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
