
using HealthR.Data.Models.Scheduler;

namespace HealthR.Services.Data.Models
{
    public class AppointmentRequestServiceModel
    { 

        public AppointmentServiceModel RequestedAppointment { get; set; } 

        public AppointmentServiceModel ConflictedAppointment { get; set; } 
        
    }
}
