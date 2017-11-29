using LearningSystem.Data.Models;
using LearningSystem.Service;
using LearningSystem.Web.Infrastructures.Extensions;
using LearningSystem.Web.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Controllers
{
    public class CoursesController : Controller
    {
        public readonly ICourseService courses;
        public readonly UserManager<User> userManager;

        public CoursesController(ICourseService courses, UserManager<User> userManager)
        {
            this.courses = courses;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Details(int id)
        {
            var model = new CourseDetailsViewModel
            {
                Course = await this.courses.ByIdAsync(id)
            };
            
            if(model.Course == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);
                model.UserIsSignedInCourse = await this.courses.UserIsSignedInCourseAsync(id, userId);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignIn(int id)
        {

            var userId = this.userManager.GetUserId(User);

            var success = await this.courses.SignInUserAsync(id, userId);

            if(!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Thank you for your registration!");
            return RedirectToAction(nameof(Details), new { id });


        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Signout(int id)
        {

            var userId = this.userManager.GetUserId(User);

            var success = await this.courses.SignOutUserAsync(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Think twice you are welcome again!");

            return RedirectToAction(nameof(Details), new { id });


        }
    }
}

