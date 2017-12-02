using System.Threading.Tasks;
using LearningSystem.Data;
using LearningSystem.Service.Models;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LearningSystem.Data.Models;

namespace LearningSystem.Service.Implementations
{
   public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

       public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;

        }

        public async Task<bool> AddGrade(int courseId, string studentId, Grade grade)
        {
            var studentInCourse = await this.db.FindAsync<StudenCourse>(courseId, studentId);
            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;
            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> CoursesAsync(string trainerId)
            => await this.db
            .Courses
            .Where(c => c.TrainerId == trainerId)
            .ProjectTo<CourseListingServiceModel>()
            .ToListAsync();

        public async Task<bool> IsTrainer(int courseId, string trainerId)
            => await this.db
            .Courses
            .AnyAsync(c => c.Id == courseId && c.TrainerId == trainerId);

        public async Task<IEnumerable<StudentInCourseServiceModel>> StudentInCourseAsync(int courseId)
         => await this.db
            .Courses
            .Where(c => c.Id == courseId)
            .SelectMany(c => c.Students.Select(s => s.Student))
            .ProjectTo<StudentInCourseServiceModel>(new { courseId })
            .ToListAsync();
    }
}
