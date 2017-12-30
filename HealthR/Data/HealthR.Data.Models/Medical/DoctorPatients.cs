
namespace HealthR.Data.Models.Medical
{
   public class DoctorPatients
    {
        public string DoctorId { get; set; }

        public User Doctor { get; set; }

        public string PatientId { get; set; }

        public User Patient { get; set; }
    }
}
