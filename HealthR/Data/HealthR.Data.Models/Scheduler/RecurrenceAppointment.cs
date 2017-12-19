

namespace HealthR.Data.Models.Scheduler
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RecurrenceAppointment : Appointment
    {
       
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public int Occurrences { get; set; }

        [Required]
        public RecurrenceType RecurrenceType { get; set; }

        [Required]
        public DayOfWeek DayOfWeekMask { get; set; }

        [Required]
        public RecurrenceEndType RecurrenceEndType { get; set; }
    }
}
