
namespace HealthR.Web.ViewModels.MedicalSheet
{
    using HealthR.Services.Data.Doctor.Models;
   

    public class MedicalSheetViewModel
    {
        public DoctorPatientDetailsServiceModel Patient { get; set; }

        public DoctorPatientDetailsServiceModel Doctor { get; set; }

        public string ExaminationDescription { get; set; }
    }
}
