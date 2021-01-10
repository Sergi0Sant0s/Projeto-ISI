using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([Bind("Username,Password")] Login @utilizador)
        {
            try
            {
                if (utilizador == null || string.IsNullOrEmpty(utilizador.Username) || string.IsNullOrEmpty(utilizador.Password))
                    return RedirectToAction("Login", "Home");

                ClaimsIdentity identity = null;
                Auth_Token tk = null;
                bool isAuthenticate = false;
                if((tk = await LoginRepository.Authenticate(utilizador)) != null)
                {
                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name,tk.Username),
                        new Claim(ClaimTypes.Role,tk.Role),
                        new Claim(ClaimTypes.Expiration, tk.Expiration.ToString()),
                        new Claim(ClaimTypes.Rsa, tk.Token)
                        }, CookieAuthenticationDefaults.AuthenticationScheme);
                    //
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("~/Views/Home/Error.cshtml", new Error() { Title = "Autenticação", Message = e.Message });
            }

        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("~/Views/Home/Error.cshtml", new Error() { Title = "Autenticação", Message = e.Message });
            }
        }
    }
}
