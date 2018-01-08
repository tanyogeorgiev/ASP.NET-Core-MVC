

namespace HealthR.Web.ViewModels.Users
{
    using HealthR.Services.Data.Models;

    public class AppointmentRequestsProfileViewModel
    { 
        public AppointmentServiceModel RequestedAppointment { get; set; }

        public AppointmentServiceModel ConflictedAppointment { get; set; }
    }
}
