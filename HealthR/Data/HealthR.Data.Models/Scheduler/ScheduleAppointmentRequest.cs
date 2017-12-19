namespace HealthR.Data.Models.Scheduler
{
   public class ScheduleAppointmentRequest
    {

        public Schedule RequestedSchedule { get; set; }

        public int RequestedScheduleId { get; set; }


        public Appointment AppointmentRequest { get; set; }

        public int AppointmentRequestId { get; set; }

        public bool Confirm { get; set; } = false;
    }
}
