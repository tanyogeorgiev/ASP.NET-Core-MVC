
namespace HealthR.Services.Data.Doctor.Models
{
    using HealthR.Common.Mapping;
    using HealthR.Data.Models;

    public class DoctorPatientServiceModel : IMapFrom<User>
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }
}
