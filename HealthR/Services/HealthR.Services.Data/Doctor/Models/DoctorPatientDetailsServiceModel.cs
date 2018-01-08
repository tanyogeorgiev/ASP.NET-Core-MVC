
namespace HealthR.Services.Data.Doctor.Models
{
    using AutoMapper;
    using HealthR.Common.Mapping;
    using HealthR.Data.Models;

    public class DoctorPatientDetailsServiceModel : DoctorPatientServiceModel, IMapFrom<User>, IHaveCustomMapping
    {
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Birthdate { get; set; }

        public void ConfigureMapping(Profile mapper)
        => mapper
            .CreateMap<User, DoctorPatientDetailsServiceModel>()
            .ForMember(m => m.Birthdate, cfg => cfg.MapFrom(u => u.Birthdate.ToShortDateString()));
    }
}
