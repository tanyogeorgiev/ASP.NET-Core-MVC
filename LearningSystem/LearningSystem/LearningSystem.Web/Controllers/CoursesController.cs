﻿using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Service;
using LearningSystem.Service.Models;
using LearningSystem.Web.Infrastructures.Extensions;
using LearningSystem.Web.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
                Course = await this.courses.ByIdAsync<CourseDetailsServiceModel>(id)
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitExam(int id,IFormFile exam)
        {
            if(!exam.FileName.EndsWith(".zip")
                || exam.Length > DataConstants.CourseExamSubmissionFileLength)
            {
                TempData.AddErrorMessage("Your  file should be a '.zip' dile with no more than 2MB size!");
                return RedirectToAction(nameof(Details), new { id });
            }

            var fileContents = await exam.ToByteArrayAsync();
            var userId = this.userManager.GetUserId(User);

            var success = await this.courses.SaveExamSubmission(id, userId, fileContents);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Successfull submited exam.");
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}

