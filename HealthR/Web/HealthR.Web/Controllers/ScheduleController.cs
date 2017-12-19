

namespace HealthR.Web.Controllers
{
    using HealthR.Data.Models;
    using HealthR.Services.Data.Contracts;
    using HealthR.Web.ViewModels.Schedule;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    [Authorize]
    public class ScheduleController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IAppointmentService appointments;

        public ScheduleController(UserManager<User> userManager, IAppointmentService appointments)
        {
            this.userManager = userManager;
            this.appointments = appointments;
        }

        [HttpGet]
        public IActionResult ByWeek(AppointmentViewModel model)
        {

            var week = model.WeekNumber;
            if (week == 0)
            {
                week = GetWeek(DateTime.UtcNow);

            }
            var modelin = new AppointmentViewModel
            {
                FirstDayOfWeek = GetDateByWeek(week),
                WeekNumber = week
            };

            return View(modelin);
        }




        [HttpPost]
        public async Task<JsonResult> NewAppointment(AppointmentViewModel model)
        {
            var userId =  this.userManager.GetUserId(User);

            var result = await this.appointments.AddAppointment(
                model.Title,
                model.Description,
                Convert.ToDateTime(model.StartDate),
                userId);

            var jsonResult = new
            {
                success = result
            };

            return Json(jsonResult);
        }





        private DateTime GetDateByWeek(int week)
        {

            int weekNumber = week;
            // mult by 7 to get the day number of the year
            int days = (weekNumber - 1) * 7;
            // get the date of that day
            DateTime dt1 = GetFirstDayOfCurrentYear().AddDays(days);
            // check what day of week it is
            DayOfWeek dow = dt1.DayOfWeek;
            // to get the first day of that week - subtract the value of the DayOfWeek enum from the date
            DateTime startDateOfWeek = dt1.AddDays(-(int)dow + 1);

            return startDateOfWeek;
        }

        private int GetLastWeekOfCurrentYear()
        {
            var currentDate = DateTime.UtcNow;
            var lastDayOfCurrentYear = new DateTime(currentDate.Year, 12, 31);
            return GetWeek(lastDayOfCurrentYear);

        }

        private DateTime GetFirstDayOfCurrentYear()
        {
            return new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
        }

        private int GetWeek(DateTime date)

        {
            var currentDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var culture = CultureInfo.CurrentCulture;
            var firstdayofweek = DayOfWeek.Sunday;
            return culture.Calendar.GetWeekOfYear(
            date,
            culture.DateTimeFormat.CalendarWeekRule,
            firstdayofweek);
        }


    }
}
