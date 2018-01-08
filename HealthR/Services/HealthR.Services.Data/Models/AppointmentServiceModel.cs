using AutoMapper;
using HealthR.Common.Mapping;
using HealthR.Data.Models.Scheduler;

namespace HealthR.Services.Data.Models
{
  public  class AppointmentServiceModel : IMapFrom<Appointment>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string StartTime { get; set; }
        
        public string PatientName { get; set; }

        public string PatientId { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }

        public void ConfigureMapping(Profile mapper)
        => mapper
            .CreateMap<Appointment, AppointmentServiceModel>()
            .ForMember(u => u.CreatorName, cfg => cfg.MapFrom(p => p.Creator.Name));
    }
}
