

namespace HealthR.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using HealthR.Data;
    using HealthR.Data.Models.Scheduler;
    using System.Linq;

    public class AppointmentService : IAppointmentService
    {
        private readonly HealthRDbContext db;

        public AppointmentService(HealthRDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddAppointment(string title, string description, DateTime startTime, string userId)
        {
            var appointment =   new Appointment
            {
                Title = title,
                Description = description,
                StartTime = startTime
            };

            this.db.Add(appointment);

            var scheduleId = (int)this.db.Users.Where(a => a.Id == userId).Select(u => u.ScheduleId).FirstOrDefault();

            var scheduleAppointment = new ScheduleAppointment
            {
                ScheduleId = scheduleId,
                AppointmentId = appointment.Id
            };

            this.db.Add(scheduleAppointment);

            await this.db.SaveChangesAsync();

            return true;
        }


    }
}
