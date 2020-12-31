using Client.Models;
using Client.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        public async Task<IActionResult> Authentication([Bind("Username,Password")] Login @utilizador)
        {
            try
            {
                Program.Authentication = utilizador;
                await LoginRepository.Authenticate();
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
                Program.Token = null;
                Program.Authentication = null;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("~/Views/Home/Error.cshtml", new Error() { Title = "Autenticação", Message = e.Message });
            }
        }
    }
}
