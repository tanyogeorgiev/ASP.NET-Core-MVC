using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthR.Web
{
    public class WebConstants
    {

        public const string AdminArea = "Admin";
        public const string DoctorArea = "Doctor";


        public const string AdminEmail = "admin@domain.com";
        public const string AdminPassword = "test123";



        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";


        public const string AdministratorRole = "Administrator";
        public const string DoctorRole = "Dctor";

        
        public const string AppointmentCreateSuccessMessage =    "Appointment was created successfully";
        public const string AppointmentCreateFailMessage =       "Appointment was NOT created successfully";
        public const string AppointmentNotDeletedMessage =       "Appointment was NOT deleted!";
        public const string AppointmentDeleteSuccessMessage =    "Appointment was deleted successfully";
        public const string AppointmentEditSuccessMessage =      "Appointment was edited successfully";
        public const string AppointmentDateTimeReservedMessage = "Already has appointment for this date and time!";

        public const string ScheduleCreateSucceess = "Your new schedule was created successfully!";


    }
}
