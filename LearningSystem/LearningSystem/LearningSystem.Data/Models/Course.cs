
namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.CourseMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.CourseDescriptionMaxLength)]
        public string Description { get; set; }

        public string TrainerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public User Trainer { get; set; }

        public List<StudenCourse> Students { get; set; } = new List<StudenCourse>();
    }
}
