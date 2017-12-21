

namespace HealthR.Data.Models.Scheduler
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.AppointmentTitleMaxLength)]
        [MinLength(DataConstants.AppointmentTitleMinLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<AppointmentLabel> Labels { get; set; } = new List<AppointmentLabel>();

        public List<ScheduleAppointment> Schedules { get; set; } = new List<ScheduleAppointment>();

        public List<ScheduleAppointmentRequest> RequestedSchedules { get; set; } = new List<ScheduleAppointmentRequest>();

        public bool IsDeleted { get; set; } = false;


    }
}
