

namespace HealthR.Services.Data.Models
{
    using AutoMapper;
    using HealthR.Common.Mapping;
    using HealthR.Data.Models;
    using System;
    using System.Collections.Generic;

    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string Email { get; set; }

        public string DoctorName { get; set; }

        public IEnumerable<UserSchedulesServiceModel> Schedules { get; set; }
        
        public IEnumerable<UserMedicalSheetsServiceModel> MedicalSheets { get; set; }

        public void ConfigureMapping(Profile mapper)
          => mapper
            .CreateMap<User, UserProfileServiceModel>()
            .ForMember(u => u.MedicalSheets, cfg => cfg.MapFrom(s => s.MedicalSheets))
            .ForMember(u => u.DoctorName, cfg => cfg.MapFrom(s => s.Doctor.Name))
            .ForMember(u => u.Schedules, cfg => cfg.MapFrom(s => s.Schedules));
    }
}
