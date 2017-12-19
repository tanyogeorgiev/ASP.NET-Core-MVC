
namespace HealthR.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IAppointmentService
    {
        Task<bool> AddAppointment(
            string title,
            string description,
            DateTime startTime,
            string scheduleId);
    }
}
