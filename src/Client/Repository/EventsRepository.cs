﻿using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class EventsRepository
    {
        const string get_method = "Events";
        const string getTeamsByEvent_method = "Events/GetTeamsByEventId";
        const string post_method = "Events";
        const string AddTeamToEvent_method = "Events/AddTeamToEvent";
        const string RemoveTeamFromEvent_method = "Events/RemoveTeamFromEvent";
        const string put_method = "Events";
        const string delete_method = "Events";


        public async Task<List<Event>> GetAllEvents()
        {
            List<Event> events = null;
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
                                events = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                        else
                        {
                            //ERROR
                            return events;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return events;
        }

        public async Task<Event> GetEventById(int? id)
        {
            Event ev = null;
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
                            ev = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
                            List<Team> tms = await GetTeamsByEventId(ev.EventId);
                            ev.Teams = tms == null ? new List<Team>() : tms;
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

            return ev;
        }

        public async Task<List<Team>> GetTeamsByEventId(int? id)
        {

            List<Team> tms = null;
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
                        HttpResponseMessage response = await client.GetAsync(getTeamsByEvent_method + $"/{id}");

                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            tms = new List<Team>();
                        }
                        else
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                //Finded
                                tms = JsonConvert.DeserializeObject<List<Team>>(await response.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                //ERROR
                                return null;
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tms;
        }
        public async Task<bool> AddTeamToEvent(HttpContext ctx, Event ev, Team addTeam)
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
                        HttpResponseMessage response = await client.PostAsync(AddTeamToEvent_method + $"?idEvent={ev.EventId}&idTeam={addTeam.TeamId}",new StringContent(string.Empty));

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

        public async Task<bool> RemoveTeamFromEvent(HttpContext ctx, Event ev, Team addTeam)
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
                        HttpResponseMessage response = await client.PostAsync(RemoveTeamFromEvent_method + $"?idEvent={ev.EventId}&idTeam={addTeam.TeamId}", new StringContent(string.Empty));

                        if (response.IsSuccessStatusCode)
                        {
                            //Removed
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

        public async Task<Event> AddNewEvent(HttpContext ctx, Event newEvent)
        {
            Event ev = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newEvent), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            ev = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
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
            return ev;
        }

        public async Task<Event> EditEvent(HttpContext ctx, Event editEvent, int? id)
        {
            Event ev = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editEvent), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            ev = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
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
            return ev;
        }

        public async Task<bool> DeleteEvent(HttpContext ctx, int? id)
        {
            Event deleteEvent = await GetEventById(id);
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
