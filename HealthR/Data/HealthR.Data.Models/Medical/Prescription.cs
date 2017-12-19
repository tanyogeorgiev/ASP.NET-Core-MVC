namespace HealthR.Data.Models.Medical
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Prescription
    {
        public int Id { get; set; }

        [Required]
        public Patient Patient { get; set; }

        public string  PatientId { get; set; }

        [Required]
        public Doctor Doctor { get; set; }

        public string DoctorId { get; set; }

        public DateTime IssueDate { get; set; }

        public bool MultipleUse { get; set; }

        public List<PrescriptionMedicament> Medicaments { get; set; } = new List<PrescriptionMedicament>();




    }
}
