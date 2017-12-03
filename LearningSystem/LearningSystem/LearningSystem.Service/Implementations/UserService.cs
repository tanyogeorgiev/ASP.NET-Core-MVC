

namespace LearningSystem.Service.Implementations
{
    using System.Threading.Tasks;
    using LearningSystem.Service.Models;
    using LearningSystem.Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using LearningSystem.Data.Models;
    using iTextSharp.text;
    using System.IO;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using System;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext db;
        private readonly IPdfGenerator pdfGenerator;

        public UserService(LearningSystemDbContext db,
            IPdfGenerator pdfGenerator)
        {
            this.db = db;
            this.pdfGenerator = pdfGenerator;
        }

        public async Task<IEnumerable<UserListingServiceModel>> FindAsync(string search)
        {
            search = search ?? string.Empty;
            return await this.db
                .Users
                .OrderBy(u => u.UserName)
                .Where(u => u.UserName.ToLower().Contains(search.ToLower()))
                .ProjectTo<UserListingServiceModel>()
                .ToListAsync();
        }

        public async Task<byte[]> GetPdfCertificate(int courseId, string studentId)
        {
            var studentInCourse = this.db.FindAsync<StudenCourse>(courseId, studentId);

            if(studentInCourse == null)
            {
                return null;
            }

            var data = await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseStartDate = c.StartDate,
                    CourseEndDate = c.EndDate,
                    StudentName = c.Students.Where(s => s.StudentId == studentId).Select(st => st.Student.Name).FirstOrDefault(),
                    StudentGrade = c.Students.Where(s => s.StudentId == studentId).Select(st => st.Grade).FirstOrDefault(),
                    CourseTrainer = c.Trainer.Name,

                }).FirstOrDefaultAsync();

            return this.pdfGenerator.GeneratePdfFromThml(string.Format(ServiceConstants.PdfCertificateFormat,
                data.CourseName,
                data.CourseStartDate.ToShortDateString(),
                data.CourseEndDate.ToShortDateString(),
                data.StudentName,
                data.StudentGrade,
                data.CourseTrainer,
                DateTime.UtcNow.ToShortDateString()));
        }

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
            => await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserProfileServiceModel>(new { studentId = id })
                .FirstOrDefaultAsync();
    }
}
