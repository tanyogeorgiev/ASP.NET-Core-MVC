﻿

namespace LearningSystem.Service.Admin.Implementations
{
    using System;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using System.Threading.Tasks;

    public class AdminCoursesService : IAdminCoursesService
    {
        private readonly LearningSystemDbContext db;

        public AdminCoursesService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task Create (string name, string description, DateTime startDate, DateTime endDate, string trainerId)
        {
            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            this.db.Add(course);

            await this.db.SaveChangesAsync();
        }
    }
}
