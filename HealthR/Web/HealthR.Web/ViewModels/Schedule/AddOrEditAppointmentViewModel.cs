using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthR.Web.ViewModels.Schedule
{
    public class AddOrEditAppointmentViewModel : AppointmentViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Change appointment date to:(MM/dd/yyyy)")]
        public DateTime NewStartDay { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Change appointment time to:")]
        public DateTime NewStartTime { get; set; }
    }
}
