
namespace HealthR.Data.Models
{
    using Scheduler;

    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        [Required]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public int? ScheduleId { get; set; }

        public Schedule Schedule { get; set; } 

    }
}
