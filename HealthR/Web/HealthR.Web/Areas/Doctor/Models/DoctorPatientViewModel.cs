namespace HealthR.Web.Areas.Doctor.Models
{
    using HealthR.Common.Mapping;
    using HealthR.Services.Data.Doctor.Models;
    using System;

    public class DoctorPatientViewModel : IMapFrom<DoctorPatientServiceModel>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
