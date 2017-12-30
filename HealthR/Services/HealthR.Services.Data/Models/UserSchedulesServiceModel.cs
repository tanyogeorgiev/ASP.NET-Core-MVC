
namespace HealthR.Services.Data.Models
{
    using AutoMapper;
    using HealthR.Common.Mapping;
    using HealthR.Data.Models.Scheduler;

    public class UserSchedulesServiceModel: IMapFrom<Schedule>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int AppointmentsCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Schedule, UserSchedulesServiceModel>()
            .ForMember(u => u.AppointmentsCount, cfg => cfg.MapFrom(s => s.Appointments.Count));
    }
}
