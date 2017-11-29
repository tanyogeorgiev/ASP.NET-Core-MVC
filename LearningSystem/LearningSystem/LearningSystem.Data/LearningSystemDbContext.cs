﻿
namespace LearningSystem.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
            if (options == null)
            {
                throw new System.ArgumentNullException(nameof(options));
            }
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Article> Articles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<StudenCourse>()
                .HasKey(sc => new { sc.CourseId, sc.StudentId });

            builder
                .Entity<StudenCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId);


            builder
                .Entity<StudenCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId);

            builder
                .Entity<Course>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.Trainings)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(a => a.Articles)
                .HasForeignKey(a => a.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
