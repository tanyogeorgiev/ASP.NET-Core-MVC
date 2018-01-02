
namespace HealthR.Web.Controllers
{
    using HealthR.Data.Models;
    using HealthR.Services.Data.Contracts;
    using HealthR.Web.Infrastructure.Exstensions;
    using HealthR.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UsersController : BaseController
    {
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users,
            UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public IActionResult Index() => View();


        [Authorize]
        public async Task<IActionResult> Profile(string username)
        {

            var user = await this.userManager.FindByNameAsync(username);
            var currentUser = User.Identity.Name;
            if (user == null || user.UserName.ToLower() != currentUser.ToLower())
            {
                return NotFound();
            }



            var profile = await users.ProfileAsync(user.Id);

            return View(profile);
        }

        [Authorize]
        [Route("users/CreateSchedule")]
        public IActionResult CreateSchedule() => View();

        [Authorize]
        [HttpPost]
        [Route("users/CreateSchedule")]
        public async Task<IActionResult> CreateSchedule(CreateUserScheduleViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.users.CreateSchedule(userId, model.Name);

            this.TempData.AddSuccessMessage(WebConstants.ScheduleCreateSucceess);

            return RedirectToAction(nameof(ScheduleController.ByWeek), "Schedule", new { area = string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSchedule(int id)
        {
            var currentUser = this.userManager.GetUserId(User);

            var result = await this.users.ChangeSchedule(currentUser, id);

            if (!result)
            {
                return BadRequest();
            }

            var profile = await users.ProfileAsync(currentUser);

            return RedirectToAction(nameof(Profile), profile);
        }
    }
}
