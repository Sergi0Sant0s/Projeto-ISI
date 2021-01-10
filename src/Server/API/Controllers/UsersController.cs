using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATA.Context;
using DATA.Entities;
using Microsoft.AspNetCore.Authorization;
using DATA.Jwt;
using Microsoft.Extensions.Configuration;

namespace Server.Controllers
{
    [Authorize("Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly IConfiguration _config;

        public UsersController(MyContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        /// <summary>
        /// Devolve todos os utilizadores
        /// </summary>
        /// <returns>Devolve uma lista com todos os utilizadores</returns>
        [Authorize("Bearer")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        
        /// <summary>
        /// Devolve um utilizador pela id
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns>Retorna um utilizador</returns>
        [Authorize("Bearer")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        /// <summary>
        /// Edita um utilizador
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <param name="user">objeto com o utilizador alterado</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        /// <summary>
        /// Adiciona novo utilizador
        /// </summary>
        /// <param name="user">objeto com o novo utilizador</param>
        /// <returns>Retorna um objeto com o novo utilizador</returns>
        [Authorize("Bearer")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }


        /// <summary>
        /// Elimina um utilizador pela id
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns>Retorna o resultado</returns>
        [Authorize("Bearer")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Devolve todas as roles existentes
        /// </summary>
        /// <returns>Retorna uma lista com todas as roles</returns>
        [Authorize("Bearer")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("roles")]
        public async Task<ActionResult<object>> GetRoles()
        {
            return new string[] { UserRoles.User.ToString(), UserRoles.Convidado.ToString() };
        }


        /// <summary>
        /// Verifica se o utilizador existe
        /// </summary>
        /// <param name="id">id do utilizador</param>
        /// <returns>Retorna true/false</returns>
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }


        /// <summary>
        /// Devolve a role com o nome formatado
        /// </summary>
        /// <param name="uRoles">role desformatada</param>
        /// <returns>Retorna uma string</returns>
        private string CheckUserRoles(string uRoles)
        {
            switch (uRoles.ToUpper())
            {
                case "ADMIN":
                    return UserRoles.Admin;
                case "User":
                    return UserRoles.User;
                default:
                    return null;
            }
        }
    }
}
