﻿

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

            var week = model.Week.WeekNumber;
            if (week == 0)
            {
                week = GetWeek(DateTime.UtcNow);

            }

            var userSchedule = await PrepareResult(week);

            return View(userSchedule);
        }

       

        [HttpPost]
        public async Task<IActionResult> NewAppointment(UserScheduleViewModel model)
        {

            if (String.IsNullOrEmpty(model.EditAppointment.Title)
                || String.IsNullOrEmpty(model.EditAppointment.Description)
                || String.IsNullOrEmpty(model.EditAppointment.StartTime))
            {
                this.TempData.AddErrorMessage($"Appointment was NOT created successfully");

                return RedirectToAction(nameof(ByWeek));
            }

            var userId = this.userManager.GetUserId(User);

            var result = await this.appointments.AddAppointment(
                model.EditAppointment.Title,
                model.EditAppointment.Description,
                Convert.ToDateTime(model.EditAppointment.StartTime),
                userId);

            this.TempData.AddSuccessMessage($"Appointment was created successfully");


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

            var currentStartDateTime = Convert.ToDateTime(model.EditAppointment.StartTime);

            if (newDateTime != currentStartDateTime)
            { 
            var validationChecks = await this.appointments.AlreadyScheduled(newDateTime);

         


            if (validationChecks)
            {
                this.TempData.AddErrorMessage($"Already has appointment for this date and time!");
                    TempData["appointmentId"] = model.EditAppointment.Id;
                return RedirectToAction(nameof(ByWeek));
            }

            }
            await this.appointments.Edit(
                model.EditAppointment.Id,
                model.EditAppointment.Title,
                model.EditAppointment.Description,
                newDateTime);

            



            this.TempData.AddSuccessMessage($"Appointment was edited successfully");

            var week = GetWeek(Convert.ToDateTime(model.EditAppointment.StartTime));
            var userSchedule = await PrepareResult(week);
            return View(nameof(ByWeek),userSchedule);
           
        }

        public async Task<IActionResult> DeleteAppointment (string id,string week)
        {
           
           
            if (String.IsNullOrEmpty(id)
                || String.IsNullOrEmpty(week) )
            {
                this.TempData.AddErrorMessage($"APPOINTMENT WAS NOT DELETED!");
               
            }

            var exist = await this.appointments.IsExistById(int.Parse(id));
             if (!exist)
            {
                this.TempData.AddErrorMessage($"APPOINTMENT WAS NOT DELETED!");

            }

            else
            {
                await this.appointments.DeleteById(int.Parse(id));
            }


            this.TempData.AddSuccessMessage($"Appointment was deleted successfully");

            var userSchedule = await PrepareResult(int.Parse(week));
            return View(nameof(ByWeek), userSchedule);
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
            var firstdayofweek = DayOfWeek.Sunday;
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

    }
}
