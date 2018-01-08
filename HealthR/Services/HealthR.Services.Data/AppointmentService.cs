

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
    using AutoMapper;

    public class AppointmentService : IAppointmentService
    {
        private readonly HealthRDbContext db;

        public AppointmentService(HealthRDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CheckUserForSchedule(string userId)
         => await this.db.Users.AnyAsync(u => u.Id == userId && u.ScheduleId != null);


        public async Task<bool> AddAppointment(string title, string description, DateTime startTime, string userId, string patientId)
        {
            var appointment = new Appointment
            {
                Title = title,
                Description = description,
                StartTime = startTime,
                PatientId = patientId,
                CreatorId = userId
            };

            this.db.Add(appointment);
            int scheduleId = GetScheduleId(userId);

            if (patientId != null)
            {
                int patientScheduleId = GetScheduleId(patientId);

                var patientScheduleAppointment = new ScheduleAppointmentRequest
                {
                    RequestedScheduleId = patientScheduleId,
                    AppointmentRequestId = appointment.Id,
                    Confirm = false


                };

                this.db.Add(patientScheduleAppointment);
            }

            var scheduleAppointment = new ScheduleAppointment
            {
                ScheduleId = scheduleId,
                AppointmentId = appointment.Id
            };

            this.db.Add(scheduleAppointment);

            await this.db.SaveChangesAsync();

            return true;
        }


        public async Task<AppointmentServiceModel> GetAppointmentById(int appointmentId)
        => await this.db
            .Appointments
            .Where(a => a.Id == appointmentId)
            .ProjectTo<AppointmentServiceModel>()
            .FirstOrDefaultAsync();


        public async Task<IEnumerable<AppointmentRequestServiceModel>> GetRequestedAppointments(string userId)
        {
            int scheduleId = GetScheduleId(userId);


            var requestedIds = this.db
                 .RequestedAppointments
                 .Where(ra => ra.RequestedScheduleId == scheduleId && !ra.Confirm)
                 .Select(ra => ra.AppointmentRequestId)
                 .ToList();



            var requestedAppointment = await this.db
                .Appointments
                .Where(a => requestedIds.Contains(a.Id))
                .Select(ra => new
                {
                    RequestedAppointment = ra,

                })
               .ProjectTo<AppointmentRequestServiceModel>()
               .ToListAsync();

            foreach (var appointment in requestedAppointment)
            {

                var ca = GetConflictedAppointment(Convert.ToDateTime(appointment.RequestedAppointment.StartTime), userId);

                if (ca != null)
                {
                    appointment.ConflictedAppointment = Mapper.Map<Appointment, AppointmentServiceModel>(ca);
                }

            }

            return requestedAppointment;
        }

       

        public async Task<IEnumerable<AppointmentServiceModel>> GetTodayAppointment(string userId)
        {
            var scheduleId = GetScheduleId(userId);

            var appointments = await this.db
                .Appointments
                .Where(a => a.Schedules.Any(s => s.ScheduleId == scheduleId) && !a.IsDeleted && a.StartTime.Date == DateTime.Now.Date)
                .OrderBy(ap => ap.StartTime)
                .ProjectTo<AppointmentServiceModel>()
                .ToListAsync();

            return appointments;

        }

        public async Task<bool> AlreadyScheduled(DateTime startDate, string userId)
        {

            var scheduleId = GetScheduleId(userId);

            var appointment = await this.db.Schedules.Where(s => s.Id == scheduleId && s.Appointments.Any(ap => ap.Appointment.StartTime == startDate && !ap.
             Appointment.IsDeleted)).FirstOrDefaultAsync();

            if (appointment != null)
            {
                return true;

            }
            return false;
            //this.db.Appointments.AnyAsync(ap => ap.StartTime == startDate && !ap.IsDeleted && ap.Schedules.Any(s=>s.ScheduleId == scheduleId));
        }

        public async Task DeleteById(int id, string userId)
        {
            var appointment = this.db.Appointments.Find(id);

            if (appointment.CreatorId == userId)
            {
                appointment.IsDeleted = true;
            }
            else
            {
                var scheduleId = GetScheduleId(userId);

                var userAppointment = this.db.ScheduleAppointments.Find(scheduleId, id);
                this.db.ScheduleAppointments.Remove(userAppointment);
            }


            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string title, string Description, DateTime startTime, string patientId)
        {
            var appointment = this.db.Appointments.Find(id);




            if (patientId != null && patientId != appointment.PatientId)
            {
                if (appointment.PatientId != null)
                {

                    var oldPatientScheduleId = GetScheduleId(appointment.PatientId);
                    var oldUserAppointment = this.db.ScheduleAppointments.Find(oldPatientScheduleId, appointment.Id);
                    if (oldUserAppointment != null)
                    {
                        this.db.ScheduleAppointments.Remove(oldUserAppointment);
                    }
                    var oldUserRequestAppointment = this.db.RequestedAppointments.Find(oldPatientScheduleId, appointment.Id);

                    if (oldUserRequestAppointment != null)
                    {
                        this.db.RequestedAppointments.Remove(oldUserRequestAppointment);

                    }

                }

                int patientScheduleId = GetScheduleId(patientId);
                var patientAlreadyRequested = this.db.RequestedAppointments.Find(patientScheduleId, appointment.Id);
                if (patientAlreadyRequested != null)
                {
                    patientAlreadyRequested.Confirm = false;
                }
                else
                {
                    var patientScheduleAppointment = new ScheduleAppointmentRequest
                    {
                        RequestedScheduleId = patientScheduleId,
                        AppointmentRequestId = appointment.Id,
                        Confirm = false
                    };

                    this.db.Add(patientScheduleAppointment);
                }
            } 

            appointment.Title = title;
            appointment.Description = Description;
            appointment.StartTime = startTime;
            appointment.PatientId = patientId;
            await this.db.SaveChangesAsync();


        }
        public async Task<bool> AcceptAppointment(string userId, int appointmentId, int conflictedId)
        {
            var scheduleId = GetScheduleId(userId);
            var appointmentExist = await IsExistById(appointmentId);
            if (!appointmentExist)
            {
                return false;
            }
            if (conflictedId != 0)
            {
                var conflictedExist = await IsExistById(conflictedId);
                if (!conflictedExist)
                {
                    return false;
                }

                var conflictedAppointment = this.db.ScheduleAppointments.Find(scheduleId, conflictedId);

                this.db.ScheduleAppointments.Remove(conflictedAppointment);

            }
            var acceptedAppoitnemnt = new ScheduleAppointment()
            {
                ScheduleId = scheduleId,
                AppointmentId = appointmentId
            };


            this.db.ScheduleAppointments.Add(acceptedAppoitnemnt);

            var requestedAppointment = this.db.RequestedAppointments.Find(scheduleId, appointmentId);

            requestedAppointment.Confirm = true;

            await this.db.SaveChangesAsync();

            return true;

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
            =>  this.db.Appointments.Any(ap => ap.Id == id);



        private int GetScheduleId(string userId)
            => (int)this.db.Users.Where(a => a.Id == userId).Select(u => u.ScheduleId).FirstOrDefault();

        private Appointment GetConflictedAppointment(DateTime startTime, string userId)
        {
            int scheduleId = GetScheduleId(userId);

            var conflictAppointment = AlreadyScheduled(startTime, userId);
            conflictAppointment.Wait();
            if (!conflictAppointment.Result)
            {
                return null;
            }

            var result = this.db.Schedules.Where(s => s.Id == scheduleId)
                        .Select(a => a.Appointments.Where(ap => ap.Appointment.StartTime == startTime).Select(f => f.Appointment).FirstOrDefault()).FirstOrDefault();


            return result;
        }

    }
}
