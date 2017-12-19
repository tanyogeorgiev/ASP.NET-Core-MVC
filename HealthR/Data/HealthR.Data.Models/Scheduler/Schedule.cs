
namespace HealthR.Data.Models.Scheduler
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ScheduleNameMaxLength)]
        public string Name { get; set; }
        
        public string OwnerId { get; set; }

        [Required]
        public User Owner { get; set; }

        public List<ScheduleAppointment> Appointments { get; set; } = new List<ScheduleAppointment>();

        public List<ScheduleAppointmentRequest> AppointmentRequests { get; set; } = new List<ScheduleAppointmentRequest>();
    }
}
