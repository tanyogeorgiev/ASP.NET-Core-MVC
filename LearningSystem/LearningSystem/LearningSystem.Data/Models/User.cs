
namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public List<Course> Trainings { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();

        public List<StudenCourse> Courses { get; set; } = new List<StudenCourse>();
    }
}
