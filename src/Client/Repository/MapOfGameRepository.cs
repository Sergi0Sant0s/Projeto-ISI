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

        public async Task<List<Game>> GetAllMapOfGames()
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
                                    //aux.TeamA = games.GetGameByEventId(aux.TeamAid).Result;
                                    //aux.TeamB = games.GetTeamById(aux.TeamBid).Result;
                                    //aux.TeamWinner = games.GetTeamById(aux.TeamWinnerId).Result;
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
                            //gm.TeamA = games.GetTeamById(gm.TeamAid).Result;
                            //gm.TeamB = games.GetTeamById(gm.TeamBid).Result;
                            //gm.TeamWinner = games.GetTeamById(gm.TeamWinnerId).Result;
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

        public async Task<List<Game>> GetMapOfGameByGameId(int? id)
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
                        HttpResponseMessage response = await client.GetAsync(getMapOfMapOfGamesByGame_method + $"/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Finded
                            gm = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
                            foreach (var aux in gm)
                            {
                                /*aux.TeamA = games.GetTeamById(aux.TeamAid).Result;
                                aux.TeamB = games.GetTeamById(aux.TeamBid).Result;
                                if (aux.TeamWinnerId != null)
                                    aux.TeamWinner = games.GetTeamById(aux.TeamWinnerId).Result;*/
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
                            /*gm.TeamA = games.GetTeamById(gm.TeamAid).Result;
                            gm.TeamB = games.GetTeamById(gm.TeamBid).Result;
                            gm.TeamWinner = games.GetTeamById(gm.TeamWinnerId).Result;*/
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
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Program.Token.Token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //PUT
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editEvent), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            /*gm = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
                            gm.TeamA = games.GetTeamById(gm.TeamAid).Result;
                            gm.TeamB = games.GetTeamById(gm.TeamBid).Result;
                            gm.TeamWinner = games.GetTeamById(gm.TeamWinnerId).Result;*/
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
