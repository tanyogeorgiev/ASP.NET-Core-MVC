

namespace HealthR.Services.Data.Contracts
{
    using HealthR.Services.Data.Models;
    using System.Threading.Tasks;

    public interface IUserService
    {

        Task CreateSchedule(string userId, string name);
        Task<UserProfileServiceModel> ProfileAsync(string id);
        Task<bool> ChangeSchedule(string currentUser, int id);
    }
}
