
namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Service;
    using LearningSystem.Service.Models;
    using LearningSystem.Web.Infrastructures.Extensions;
    using LearningSystem.Web.Models.Trainer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize(Roles = WebConstants.TrainerRole)]
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainers;
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public TrainerController(ITrainerService trainers, UserManager<User> userManager, ICourseService courses)
        {
            this.trainers = trainers;
            this.courses = courses;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Courses()
        {
            var userId = this.userManager.GetUserId(User);
            var courses = await this.trainers.CoursesAsync(userId);

            return View(courses);
        }


        public async Task<IActionResult> Students(int id)
        {
            var userId = this.userManager.GetUserId(User);
            if (!await this.trainers.IsTrainer(id, userId))
            {
                return NotFound();
            }

            return View(new StudentInCourseViewModel()
            {
                Students = await this.trainers.StudentInCourseAsync(id),
                Course = await this.courses.ByIdAsync<CourseListingServiceModel>(id)
            });
        }

        [HttpPost]
        public async Task<IActionResult> GradeStudent(int id, string studentId, Grade grade)
        {
            if (studentId == null)
            {
                return BadRequest();
            }
            var userId = this.userManager.GetUserId(User);
            if (!await this.trainers.IsTrainer(id, userId))
            {
                return BadRequest();
            }

            var success = await this.trainers.AddGrade(id, studentId, grade);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Students), new { id });

        }

        public async Task<IActionResult> DownloadExam(int id, string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return BadRequest();
            }

            var userId = this.userManager.GetUserId(User);
            if (!await this.trainers.IsTrainer(id, userId))
            {
                return BadRequest();
            }

            var submissionContent = await this.trainers.GetExamSubmissionAsync(id, studentId);

            if (submissionContent == null)
            {
                TempData.AddErrorMessage("Something get wrong. We can find any submission for the user in this course!");
                return RedirectToAction(nameof(Students), new { id });
            }

            var studentInCourseNames = await this.trainers.StudentInCourseNamesAsync(id, studentId);

            if(studentInCourseNames == null)
            {
                TempData.AddErrorMessage("Something get wrong. We can find any submission for the user in this course!");
                return RedirectToAction(nameof(Students), new { id });
            }

            return File(submissionContent,"application/zip", $"ExamSubmission-{studentInCourseNames.CourseTitle}-{studentInCourseNames.Username}-{DateTime.UtcNow.ToString("yyyyMMdd")}.zip");
        }
        
    }
}
