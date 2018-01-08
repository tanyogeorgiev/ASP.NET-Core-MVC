using HealthR.Services.Data.Doctor.Models;

namespace HealthR.Web.Areas.Doctor.Models
{
    public class DoctorMedicalSheetViewModel
    {
 
        public DoctorPatientDetailsServiceModel Patient { get; set; }

        public DoctorPatientDetailsServiceModel Doctor { get; set; }
 
        public string ExaminationDescription { get; set; }

        
    }
}
