﻿using LearningSystem.Data.Models;
using LearningSystem.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningSystem.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users,
            UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public async Task <IActionResult> Profile (string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            if(user == null)
            {
                return NotFound();
            }

            var profile = await users.ProfileAsync(user.Id);

            return View(profile);
        }
    }
}