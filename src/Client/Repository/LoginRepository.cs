using Client.Models;
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
    public class LoginRepository
    {
        const string post_login = "Login";

        public static async Task<Auth_Token> Authenticate(Login utilizador)
        {
            Auth_Token tk = null;
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(Program.Url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //POST
                    var stringContent = new StringContent(JsonConvert.SerializeObject(utilizador), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(post_login, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        //Added
                        tk = JsonConvert.DeserializeObject<Auth_Token>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        //ERROR
                        return tk;
                    }
                }
            }
            return tk;
        }
    }
}
