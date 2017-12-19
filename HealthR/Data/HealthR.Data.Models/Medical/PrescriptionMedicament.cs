namespace HealthR.Data.Models.Medical
{
    public class PrescriptionMedicament
    {
        public Prescription Prescription { get; set; }

        public int PrescriptionId { get; set; }

        public Medicament Medicament { get; set; }

        public int MedicamentId { get; set; }
    }
}
