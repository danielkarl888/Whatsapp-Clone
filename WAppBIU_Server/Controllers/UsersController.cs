using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using WAppBIU_Server.Data;
using Services;

namespace WAppBIU_Server.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("UserName,DisplayName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _service.AddUser(user.UserName, user.Password, user.DisplayName);
                return RedirectToAction(nameof(Login));
            }
            return BadRequest();
        }
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserName,DisplayName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _service.AddUser(user.UserName, user.Password, user.DisplayName);
                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

    }
}
