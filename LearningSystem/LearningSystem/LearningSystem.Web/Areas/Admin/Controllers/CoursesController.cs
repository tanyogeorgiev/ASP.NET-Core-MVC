

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Web.Areas.Admin.Models.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Service.Admin;
    using LearningSystem.Web.Controllers;
    using LearningSystem.Web.Infrastructures.Extensions;

    public class CoursesController : BaseAdminController
    {
        private readonly UserManager<User> userManager;

        private readonly IAdminCoursesService courses;

        public CoursesController(UserManager<User> userManager, IAdminCoursesService courses)
        {
            this.userManager = userManager;
            this.courses = courses;
        }


        public async Task<IActionResult> Create()
        {
            
            return View(new AddCourseFormModel
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Trainers = await GetTrainers(),
            });            
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = await GetTrainers();
                return View(model);
            }
            var endDate = model.EndDate;

            await this.courses.Create(model.Name,
                model.Description,
                model.StartDate,
                new DateTime(endDate.Year,endDate.Month,endDate.Day,23,59,59),
                model.TrainerId);
            TempData.AddSuccessMessage($"Course {model.Name}, was created successfully");
            return  RedirectToAction(nameof(HomeController.Index), "Home",new {area =string.Empty });

        }
        

        private async Task<IEnumerable<SelectListItem>> GetTrainers()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);

            var trainerListitems = trainers.Select(t => new SelectListItem
            {
                Text = t.UserName,
                Value = t.Id
            }).ToList();

            return trainerListitems;
        }
    }
}
