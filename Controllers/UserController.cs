using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index(string searchString)
        {
            var users = userlist.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u =>
                    (!string.IsNullOrEmpty(u.Name) && u.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                ).AsQueryable();
            }

            return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Find the user by ID and return the Details view
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Show the Create view
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Assign a new ID (auto-increment)
                user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
                userlist.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the user and show the Edit view
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Update user properties
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                // Add other properties as needed
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Retrieve the user and show the Delete confirmation view
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            userlist.Remove(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
