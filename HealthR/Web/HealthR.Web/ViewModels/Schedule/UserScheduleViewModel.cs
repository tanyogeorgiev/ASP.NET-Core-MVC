

namespace HealthR.Web.ViewModels.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserScheduleViewModel
    {
        [Required]
        public IList<AppointmentViewModel> Appointments { get; set; } = new List<AppointmentViewModel>();

        public WeeksViewModel Week { get; set; } = new WeeksViewModel();


        public AddOrEditAppointmentViewModel EditAppointment { get; set; } = new AddOrEditAppointmentViewModel();

    }
}
