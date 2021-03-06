﻿

namespace LearningSystem.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LearningSystem.Service.Models;
    using LearningSystem.Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using LearningSystem.Data.Models;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;


        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }


        public async Task<IEnumerable<CourseListingServiceModel>> Active()
            => await this.db
            .Courses
            .OrderByDescending(c => c.Id)
            .Where(c => c.StartDate <= DateTime.UtcNow || DateTime.UtcNow >= c.EndDate)
            .ProjectTo<CourseListingServiceModel>()
            .ToListAsync();

        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class
            => await this.db
            .Courses
            .Where(c => c.Id == id)
            .ProjectTo<TModel>()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<CourseListingServiceModel>> FindAsync(string search)
        {
            search = search ?? string.Empty;
            return await this.db
              .Courses
              .OrderByDescending(c => c.Id)
              .Where(c => c.Name.ToLower().Contains(search.ToLower()))
              .ProjectTo<CourseListingServiceModel>()
              .ToListAsync();
        }

        public async Task<bool> SaveExamSubmission(int courseId, string studentId, byte[] examSubmission)
        {
            var studentInCourse = await this.db.FindAsync<StudenCourse>(courseId,studentId);

            if (studentInCourse == null)
            {
                return false;
            }

            studentInCourse.ExamSubmission = examSubmission;
            await this.db.SaveChangesAsync();
            return true;

        }

        public async Task<bool> SignInUserAsync(int courseId, string userId)
        {
            var course = await GetCourseInfo(courseId, userId);

            if (course == null
                || course.StartDate < DateTime.UtcNow
                || course.StudentIsSignToCourse)
            {
                return false;
            }

            var studentInCourse = new StudenCourse
            {
                StudentId = userId,
                CourseId = courseId
            };

            this.db.Add(studentInCourse);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignOutUserAsync(int id, string userId)
        {
            var course = await GetCourseInfo(id, userId);

            if (course == null
                || course.StartDate < DateTime.UtcNow
                || !course.StudentIsSignToCourse)
            {
                return false;
            }

            var studenInCourse = await this.db
                  .FindAsync<StudenCourse>(id, userId);

            this.db.Remove(studenInCourse);
            await this.db.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UserIsSignedInCourseAsync(int courseId, string userId)
            => await this.db
            .Courses
            .AnyAsync(c => c.Id == courseId && c.Students.Any(s => s.StudentId == userId));


        private async Task<CourseWithStudentInfo> GetCourseInfo(int courseId, string userId)
            => await this.db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => new CourseWithStudentInfo
                {
                    StartDate = c.StartDate,
                    StudentIsSignToCourse = c.Students.Any(s => s.StudentId == userId)
                })
            .FirstOrDefaultAsync();
    }
}
