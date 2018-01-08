

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
    using HealthR.Web.Infrastructure.Exstensions;
    using AutoMapper;
    using System.Collections.Generic;
    using HealthR.Services.Data.Models;

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
        public async Task<IActionResult> ByWeek(UserScheduleViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            var scheduleUser = await this.appointments.CheckUserForSchedule(userId);

            if (!scheduleUser)
            {
             return  RedirectToAction(nameof(UsersController.CreateSchedule), "Users", new { area = string.Empty });
            }

            var week = model.Week.WeekNumber;
            if (week == 0)
            {
                week = GetWeek(DateTime.UtcNow);

            }

            var userSchedule = await PrepareResult(week);

            return View(userSchedule);
        }

        [HttpGet]
        public async Task<JsonResult> Today()
        {
            var userId = this.userManager.GetUserId(User);

            var scheduleUser = await this.appointments.CheckUserForSchedule(userId);
            if (!scheduleUser)
            {
                RedirectToAction(nameof(UsersController.CreateSchedule), "Users", new { area = string.Empty });
            }
            var appointments = await this.appointments.GetTodayAppointment(userId);

            return Json(appointments);
        }

        [HttpGet]
        public async Task<JsonResult> AppointmentRequests()
        {
            var userId = this.userManager.GetUserId(User);

            var scheduleUser = await this.appointments.CheckUserForSchedule(userId);
            if (!scheduleUser)
            {
                RedirectToAction(nameof(UsersController.CreateSchedule), "Users", new { area = string.Empty });
            }

            var requestedAppointments = await this.appointments.GetRequestedAppointments(userId);

            return Json(requestedAppointments);
        }


        [HttpPost]
        public async Task<IActionResult> NewAppointment(UserScheduleViewModel model)
        {

            if (String.IsNullOrEmpty(model.EditAppointment.Title)
                || String.IsNullOrEmpty(model.EditAppointment.Description)
                || String.IsNullOrEmpty(model.EditAppointment.StartTime))
            {
                this.TempData.AddErrorMessage(WebConstants.AppointmentCreateFailMessage);

                return RedirectToAction(nameof(ByWeek));
            }

            var modelPatientId = model.EditAppointment.PatientId;

            string patientId = null;
            if (!String.IsNullOrEmpty(modelPatientId))
            {
                var patient = await this.userManager.FindByIdAsync(modelPatientId);
                patientId = patient.Id;
            }

            var userId = this.userManager.GetUserId(User);

            var startTime = Convert.ToDateTime(model.EditAppointment.StartTime);
            startTime = RoundTime(startTime);
            var result = await this.appointments.AddAppointment(
                model.EditAppointment.Title,
                model.EditAppointment.Description,
                startTime,
                userId,
                patientId
                );

            this.TempData.AddSuccessMessage(WebConstants.AppointmentCreateSuccessMessage);


            return RedirectToAction(nameof(ByWeek));
        }


        [HttpPost]
        public async Task<IActionResult> EditAppointment(UserScheduleViewModel model)
        {

            var newDateTime = new DateTime(
                model.EditAppointment.NewStartDay.Year,
                model.EditAppointment.NewStartDay.Month,
                model.EditAppointment.NewStartDay.Day,
                model.EditAppointment.NewStartTime.Hour,
                model.EditAppointment.NewStartTime.Minute,
                0);
             newDateTime = RoundTime(newDateTime);

            var currentStartDateTime = Convert.ToDateTime(model.EditAppointment.StartTime);

            if (newDateTime != currentStartDateTime)
            {
                string userId = this.userManager.GetUserId(User);
                var validationChecks = await this.appointments.AlreadyScheduled(newDateTime, userId);

                if (validationChecks)
                {
                    this.TempData.AddErrorMessage(WebConstants.AppointmentDateTimeReservedMessage);
                    TempData["appointmentId"] = model.EditAppointment.Id;
                    return RedirectToAction(nameof(ByWeek));
                }

            }

            await this.appointments.Edit(
                model.EditAppointment.Id,
                model.EditAppointment.Title,
                model.EditAppointment.Description,
                newDateTime,
                model.EditAppointment.PatientId);

            this.TempData.AddSuccessMessage(WebConstants.AppointmentEditSuccessMessage);

            var week = GetWeek(Convert.ToDateTime(model.EditAppointment.StartTime));
            var userSchedule = await PrepareResult(week);
            return View(nameof(ByWeek), userSchedule);

        }

       

        public async Task<IActionResult> DeleteAppointment(string id, string week)
        {

            if (String.IsNullOrEmpty(id)
                || String.IsNullOrEmpty(week))
            {
                this.TempData.AddErrorMessage(WebConstants.AppointmentNotDeletedMessage);

            }

            var exist = await this.appointments.IsExistById(int.Parse(id));
            if (!exist)
            {
                this.TempData.AddErrorMessage(WebConstants.AppointmentNotDeletedMessage);

            }

            else
            {
                var userId = this.userManager.GetUserId(User);
                await this.appointments.DeleteById(int.Parse(id),userId);
            }


            this.TempData.AddSuccessMessage(WebConstants.AppointmentDeleteSuccessMessage);

            var userSchedule = await PrepareResult(int.Parse(week));
            return View(nameof(ByWeek), userSchedule);
        }


        public async Task<JsonResult> AcceptAppointment(int appointmentId, int conflictedId)  
        {

            var userId = this.userManager.GetUserId(User);
            var result = await this.appointments.AcceptAppointment(userId, appointmentId, conflictedId);


            return  Json(result);

        }

        //    HELPER METHODS BELOW
        

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
            var firstdayofweek = DayOfWeek.Monday;
            return culture.Calendar.GetWeekOfYear(
            date,
            culture.DateTimeFormat.CalendarWeekRule,
            firstdayofweek);
        }


        private async Task<UserScheduleViewModel> PrepareResult(int week)
        {
            string userId = this.userManager.GetUserId(User);
            var appointments = await this.appointments.GetAllByUser(userId);

            var appointmentsViewModel = Mapper.Map<IEnumerable<AppointmentServiceModel>, IList<AppointmentViewModel>>(appointments);
            var weekViewModel = new WeeksViewModel
            {
                FirstDayOfWeek = GetDateByWeek(week),
                WeekNumber = week,
            };

            var userSchedule = new UserScheduleViewModel
            {
                Week = weekViewModel,
                Appointments = appointmentsViewModel

            };
            return userSchedule;
        }

        private DateTime RoundTime(DateTime newDateTime)
        {
            var minutes = newDateTime.Minute;

            if (minutes <= 7)
            {

                newDateTime = newDateTime.AddMinutes(-minutes);
            }
            else if (minutes < 20)
            {
                newDateTime = newDateTime.AddMinutes(15 - minutes);
            }
            else if (minutes < 35)
            {
                newDateTime = newDateTime.AddMinutes(30 - minutes);
            }
            else if (minutes < 50)
            {
                newDateTime = newDateTime.AddMinutes(45 - minutes);
            }
            else if (minutes > 50)
            {
                newDateTime = newDateTime.AddMinutes(60 - minutes);
            }

            return newDateTime;
        }
    }
}
