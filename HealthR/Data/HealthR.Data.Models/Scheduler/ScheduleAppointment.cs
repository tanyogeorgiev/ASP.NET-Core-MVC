namespace HealthR.Data.Models.Scheduler
{
    public class ScheduleAppointment
    {
        public  Schedule Schedule {get;set;}
        
        public int ScheduleId { get; set; }

        public Appointment Appointment { get; set; }

        public int AppointmentId { get; set; }


    }
}
