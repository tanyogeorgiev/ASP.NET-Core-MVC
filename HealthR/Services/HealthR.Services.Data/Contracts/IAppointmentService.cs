
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
            string scheduleId);


        Task<IEnumerable<AppointmentServiceModel>> GetAllByUser(string userId);
        Task Edit(int id, string title, string Description, DateTime startTime);
        Task<bool> AlreadyScheduled(DateTime startDate, string userId);
        Task DeleteById(int id);
        Task<bool> IsExistById(int id);
        Task<bool> CheckUserForSchedule(string userId);
        Task <IEnumerable<AppointmentServiceModel>> GetTodayAppointment(string userId);
    }
}
