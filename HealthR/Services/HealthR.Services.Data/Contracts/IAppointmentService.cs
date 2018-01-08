
namespace HealthR.Services.Data.Contracts
{
    using HealthR.Services.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentService
    {
        Task<bool> AddAppointment(
            string title,
            string description,
            DateTime startTime,
            string userId,
            string patientId);


        Task<IEnumerable<AppointmentServiceModel>> GetAllByUser(string userId);
        Task Edit(int id, string title, string Description, DateTime startTime, string patientId);
        Task<bool> AlreadyScheduled(DateTime startDate, string userId);
        Task DeleteById(int id,string userId);
        Task<bool> IsExistById(int id);
        Task<bool> CheckUserForSchedule(string userId);
        Task <IEnumerable<AppointmentServiceModel>> GetTodayAppointment(string userId);
        Task <AppointmentServiceModel>  GetAppointmentById (int appointmentId);
        Task<IEnumerable<AppointmentRequestServiceModel>> GetRequestedAppointments(string userId);
        Task<bool> AcceptAppointment(string userId, int appointmentId, int conflictedId);
    }
}
