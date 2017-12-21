

namespace HealthR.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using HealthR.Data;
    using HealthR.Data.Models.Scheduler;
    using System.Linq;
    using HealthR.Services.Data.Models;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class AppointmentService : IAppointmentService
    {
        private readonly HealthRDbContext db;

        public AppointmentService(HealthRDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddAppointment(string title, string description, DateTime startTime, string userId)
        {
            var appointment = new Appointment
            {
                Title = title,
                Description = description,
                StartTime = startTime
            };

            this.db.Add(appointment);
            int scheduleId = GetScheduleId(userId);

            var scheduleAppointment = new ScheduleAppointment
            {
                ScheduleId = scheduleId,
                AppointmentId = appointment.Id
            };

            this.db.Add(scheduleAppointment);

            await this.db.SaveChangesAsync();

            return true;
        }

        public Task<bool> AlreadyScheduled(DateTime startDate)
            => this.db.Appointments.AnyAsync(ap => ap.StartTime == startDate && !ap.IsDeleted);

        public async Task DeleteById(int id)
        {
            var appointment = this.db.Appointments.Find(id);

            appointment.IsDeleted = true;
    

            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string title,string Description, DateTime startTime)
        {
            var appointment = this.db.Appointments.Find(id);

            appointment.Title = title;
            appointment.Description = Description;
            appointment.StartTime = startTime;

            await this.db.SaveChangesAsync();

            
        }

        public async Task<IEnumerable<AppointmentServiceModel>> GetAllByUser(string userId)
        {
            var scheduleId = GetScheduleId(userId);

            var appointments = await this.db
                .Appointments
                .Where(a => a.Schedules.Any(s => s.ScheduleId == scheduleId) && !a.IsDeleted)
                .ProjectTo<AppointmentServiceModel>()
                .ToListAsync();

            return appointments;
        }

        public async Task<bool> IsExistById(int id)
            =>   this.db.Appointments.Any(ap => ap.Id == id);

        private int GetScheduleId(string userId)
            => (int)this.db.Users.Where(a => a.Id == userId).Select(u => u.ScheduleId).FirstOrDefault();
    }
}
