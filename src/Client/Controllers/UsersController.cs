using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Client.Repository;
using Client.Models;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        private UsersRepository contextUsers;

        public UsersController()
        {
            contextUsers = new UsersRepository();
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await contextUsers.GetAllUsers(HttpContext));
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Listagem de utilizadores", Message = e.Message });
            }
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await contextUsers.GetUserById(HttpContext, id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Detalhes do utilizador", Message = e.Message });
            }
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (var item in await contextUsers.GetAllRoles(HttpContext))
                    list.Add(new SelectListItem() { Value = item, Text = item, Selected = false });
                //
                list[0].Selected = true;
                ViewBag.Roles = new SelectList(list, "Value", "Text");
                //
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Criar utilizador", Message = e.Message });
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,Password,Role")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await contextUsers.AddNewUser(HttpContext, user);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Criar utilizador", Message = e.Message });
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                ViewBag.User = await contextUsers.GetUserById(HttpContext, id);

                List<SelectListItem> list = new List<SelectListItem>();

                foreach (var item in await contextUsers.GetAllRoles(HttpContext))
                {
                    if (ViewBag.User.Role == item)
                        list.Add(new SelectListItem() { Value = item, Text = item, Selected = true });
                    else
                        list.Add(new SelectListItem() { Value = item, Text = item, Selected = false });
                }
                ViewBag.Roles = new SelectList(list, "Value", "Text");

                if (ViewBag.User == null)
                {
                    return NotFound();
                }
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Editar utilizador", Message = e.Message });
            } 
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Email,Password,Role")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await contextUsers.EditUser(HttpContext, user, id);
                }
                catch (Exception e)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await contextUsers.GetUserById(HttpContext, id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar utilizador", Message = e.Message });
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var user = await contextUsers.GetUserById(HttpContext, id);
                if (user != null)
                    await contextUsers.DeleteUser(HttpContext, id);
                //
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("Error", new Error() { Title = "Eliminar utilizador", Message = e.Message });
            }
        }

        private bool UserExists(int id)
        {
            try
            {
                return contextUsers.GetUserById(HttpContext, id) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
