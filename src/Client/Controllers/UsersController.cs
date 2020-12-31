using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
using Client.Repository;

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
            return View(await contextUsers.GetAllUsers());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await contextUsers.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            if (Program.Token == null || Program.Authentication == null)
                return RedirectToAction("Home/Index");


            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in await contextUsers.GetAllRoles())
                    list.Add(new SelectListItem() { Value = item, Text = item, Selected = false });
            //
            list[0].Selected = true;
            ViewBag.Roles = new SelectList(list, "Value", "Text");
            //
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                await contextUsers.AddNewUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.User = await contextUsers.GetUserById(id);

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in await contextUsers.GetAllRoles())
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
                    await contextUsers.EditUser(user, id);
                }
                catch (DbUpdateConcurrencyException)
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
            if (id == null)
            {
                return NotFound();
            }

            var user = await contextUsers.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await contextUsers.GetUserById(id);
            if(user != null)
                await contextUsers.DeleteUser(id);
            //
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return contextUsers.GetUserById(id) != null;
        }
    }
}
