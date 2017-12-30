

namespace HealthR.Services.Data.Doctor.Models
{
    using AutoMapper;
    using HealthR.Common.Mapping;
    using HealthR.Data.Models;

    public class DoctorPatientAutocompleteServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Birthdate { get; set; }

        public string Email { get; set; }

        public string Label { get; set; }

        public void ConfigureMapping(Profile mapper)
       => mapper
            .CreateMap<User, DoctorPatientAutocompleteServiceModel>()
            .ForMember(m => m.Label, cfg => cfg.MapFrom(u => u.Name))
            .ForMember(m => m.Birthdate, cfg => cfg.MapFrom(u => u.Birthdate.ToShortDateString()));

    }
}
