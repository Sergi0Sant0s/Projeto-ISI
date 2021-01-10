using DATA.Context;
using DATA.Entities;
using DATA.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server.Jwt;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly IConfiguration _config;

        public LoginController(MyContext context, IConfiguration config)
        {
            this._context = context;
            this._config = config;
        }


        /// <summary>
        /// Autentica um utilizador
        /// </summary>
        /// <param name="model">utilizador com user e password</param>
        /// <returns>retorna um objeto com o token</returns>
        [HttpPost]
        public async Task<ActionResult<UserToken>> Authenticate([FromBody] Login model)
        {
            // Recupera o utilizador
            var user = _context.Users.FirstOrDefault(p => p.Username == model.Username && p.Password == model.Password);

            // Verifica se o utilizador existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user, this._config);

            // Retorna os dados
            return token;
        }
    }
}
