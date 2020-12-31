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
        TeamsRepository contextTeams;
        EventsRepository contextEvents;

        const string get_method = "Games";
        const string getGamesByEvent_method = "Games/GamesByEventId";
        const string post_method = "Games";
        const string put_method = "Games";
        const string delete_method = "Games";

        public GamesRepository()
        {
            contextTeams = new TeamsRepository();
            contextEvents = new EventsRepository();
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
                            gm.Event = contextEvents.GetEventById(gm.EventId).Result;
                            gm.TeamA = contextTeams.GetTeamById(gm.TeamAId).Result;
                            gm.TeamB = contextTeams.GetTeamById(gm.TeamBId).Result;
                            if (gm.TeamWinnerId != null)
                                gm.TeamWinner = contextTeams.GetTeamById(gm.TeamWinnerId).Result;
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
                            foreach (var item in gm)
                            {
                                item.Event = contextEvents.GetEventById(item.EventId).Result;
                                item.TeamA = contextTeams.GetTeamById(item.TeamAId).Result;
                                item.TeamB = contextTeams.GetTeamById(item.TeamBId).Result;
                                if (item.TeamWinnerId != null)
                                    item.TeamWinner = contextTeams.GetTeamById(item.TeamWinnerId).Result;
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

        public async Task<Game> AddNewGame(Game newGame)
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newGame), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
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

        public async Task<Game> EditGame(Game editGame, int? id)
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editGame), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
                            gm.Event = contextEvents.GetEventById(gm.EventId).Result;
                            gm.TeamA = contextTeams.GetTeamById(gm.TeamAId).Result;
                            gm.TeamB = contextTeams.GetTeamById(gm.TeamBId).Result;
                            if (gm.TeamWinnerId != null)
                                gm.TeamWinner = contextTeams.GetTeamById(gm.TeamWinnerId).Result;

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
