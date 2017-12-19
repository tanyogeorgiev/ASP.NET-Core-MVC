namespace HealthR.Data.Models.Medical
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Medicament
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MedicamentNameMaxLength)]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<PrescriptionMedicament> Prescriptions { get; set; } = new List<PrescriptionMedicament>();
    
    }
}
