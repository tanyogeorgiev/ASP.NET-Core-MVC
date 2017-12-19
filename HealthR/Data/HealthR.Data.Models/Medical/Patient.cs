namespace HealthR.Data.Models.Medical
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Patient : User
    {
        public Doctor Doctor { get; set; }

        public string DoctorId { get; set; }

        public List<MedicalSheet> MedicalSheets { get; set; }

        public List<Prescription> Prescriptions { get; set; }

    }
}
