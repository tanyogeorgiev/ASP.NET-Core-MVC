
namespace HealthR.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using HealthR.Data.Models;
    using HealthR.Services.Data.Admin.Contracts;
    using HealthR.Services.Data.Admin.Models;
    using HealthR.Web.Areas.Admin.ViewModels;
    using HealthR.Web.Infrastructure.Exstensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseDoctorController
    {

        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(
            IAdminUserService users,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public  async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();


            return View(new AdminUserListingsViewModel
            {
                Users = Mapper.Map<IEnumerable<AdminUserServiceModel>, IEnumerable<AdminUserViewModel>>(users),
                Roles = roles

            });
        }


        [HttpPost]
        public async Task<IActionResult> AddToRole(AddToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} syccessfully added to the {model.Role} role.");
            return RedirectToAction(nameof(Index));
        }

    }
}
