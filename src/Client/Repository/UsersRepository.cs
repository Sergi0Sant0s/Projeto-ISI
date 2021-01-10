using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
    public class UsersRepository
    {
        const string get_method = "Users";
        const string getRoles_method = "Users/roles";
        const string post_method = "Users";
        const string put_method = "Users";
        const string delete_method = "Users";


        public async Task<List<User>> GetAllUsers(HttpContext ctx)
        {
            List<User> mp = null;
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
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ctx.User.FindFirst(ClaimTypes.Rsa).Value);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        response = await client.GetAsync(get_method);

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                mp = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
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
                            return mp;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return mp;
        }

        public async Task<List<string>> GetAllRoles(HttpContext ctx)
        {
            List<string> roles = null;
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
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(ctx.User.FindFirst(ClaimTypes.Rsa).Value);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //GET
                        response = await client.GetAsync(getRoles_method);

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                roles = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
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
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return roles;
        }

        public async Task<User> GetUserById(HttpContext ctx, int? id)
        {
            User mp = null;
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
                            mp = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
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

            return mp;
        }

        public async Task<User> AddNewUser(HttpContext ctx, User newUser)
        {
            User mp = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(post_method, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Added
                            mp = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
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
            return mp;
        }

        public async Task<User> EditUser(HttpContext ctx, User editUser, int? id)
        {
            User mp = null;
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
                        var stringContent = new StringContent(JsonConvert.SerializeObject(editUser), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(put_method + $"/{id}", stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            //Edited
                            mp = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
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
            return mp;
        }

        public async Task<bool> DeleteUser(HttpContext ctx, int? id)
        {
            User deleteUser = await GetUserById(ctx, id);
            if (deleteUser != null)
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
