

namespace HealthR.Web.Areas.Doctor.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DoctorMedicalSheetFormViewModel
    {

        [Required]
        public string PatientId { get; set; }

        public string DoctorId { get; set; }

        [Required]
        public string ExaminationDescription { get; set; }
        
    }
}
