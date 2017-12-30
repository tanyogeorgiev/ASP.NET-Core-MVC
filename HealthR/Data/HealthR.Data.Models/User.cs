
namespace HealthR.Data.Models
{
    using Scheduler;

    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using HealthR.Data.Models.Medical;

    public class User : IdentityUser
    {
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        [Required]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public int? ScheduleId { get; set; }

        public User Doctor { get; set; }

        public string DoctorId { get; set; }

        
        public List<MedicalSheet> MedicalSheets { get; set; } = new List<MedicalSheet>();

        public List<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

        public List<DoctorPatients> Patients { get; set; } = new List<DoctorPatients>();

        public List<Prescription> PatientPrescriptions { get; set; } = new List<Prescription>();

        public List<MedicalSheet> PatientMedicalSheets { get; set; } = new List<MedicalSheet>();

    }
}
