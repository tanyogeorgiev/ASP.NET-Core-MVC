
namespace HealthR.Data.Models.Scheduler
{
    public class AppointmentLabel
    {
        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }

        public int LabelId { get; set; }

        public Label Label { get; set; }
    }
}
