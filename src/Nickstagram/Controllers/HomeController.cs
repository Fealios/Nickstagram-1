﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nickstagram.Models;
using Nickstagram.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nickstagram.Controllers
{
    public class HomeController : Controller
    {
        private readonly NickstagramDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public IActionResult Index()
        {
            
            return View(_db.Posts.ToList());
        }


        public HomeController (UserManager<User> userManager, SignInManager<User> signInManager, NickstagramDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPost (RegisterViewModel model)
        {
            var user = new User { UserName = model.UserName };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var thisUser = _db.Users.FirstOrDefault(users => users.UserName == model.UserName);
                return RedirectToAction("Index", "UserPage", new { id = thisUser.Id });
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Users()
        {
            UsersPageViewModel viewModel = new UsersPageViewModel
            {
                Users = _db.Users.ToList(),
                CurrentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
            return View(viewModel);
        }

    }
}
