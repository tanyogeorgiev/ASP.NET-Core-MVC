namespace HealthR.Data.Models.Medical
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Doctor : User
    {
        public List<Patient> Patients { get; set; } = new List<Patient>();

        public List<Prescription> PatientPrescriptions { get; set; } = new List<Prescription>();

        public List<MedicalSheet> PatientMedicalSheets { get; set; } = new List<MedicalSheet>();
    }
}
