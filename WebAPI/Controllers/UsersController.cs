﻿using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    //the contoller is a service (api)
    [ApiController]
    [Route("/api/[controller]/")]

    public class UsersController : Controller
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<User> Index()
        {

            return _service.GetAllUsers();
        }

        [HttpGet("{username}")]
        public IActionResult Details(string username)
        {
            if (_service.GetUser(username) != null)
            {
                return Ok(_service.GetUser(username));
            }
            else
                return NotFound();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Register/")]

        public IActionResult RegisterContact([Bind("Username,Password,Dispaly")] User user)
        {   
            if(!_service.checkValidation(user.UserName, user.Password))
            {
                    return BadRequest();
            } else
            {
                HttpContext.Session.SetString("userName", user.UserName);

                _service.AddUser(user.UserName, user.Password, user.DisplayName);
                    return NoContent();
            }
        }
        [HttpPost("/Login/")]

        public IActionResult LoginContact([Bind("Username,Password")] User user)
        {
            if (!_service.CheckUser(user.UserName, user.Password))
            {
                return BadRequest();
            }
            else
            {
                HttpContext.Session.SetString("userName", user.UserName);
                return NoContent();
            }
        }

    }

    }

