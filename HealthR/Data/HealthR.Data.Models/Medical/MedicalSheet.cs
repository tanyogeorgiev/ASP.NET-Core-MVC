﻿namespace HealthR.Data.Models.Medical
{
    using System.ComponentModel.DataAnnotations;

    public class MedicalSheet
    {
        public int Id { get; set; }

        [Required]
        public User Patient { get; set; }

        public string PatientId { get; set; }

        public User Doctor { get; set; }

        public string DoctorId { get; set; }
        
        [Required]
        public string ExaminationDescription { get; set; }

        public int PrescriptionId { get; set; }

        public Prescription Prescription { get; set; }

    }
}
