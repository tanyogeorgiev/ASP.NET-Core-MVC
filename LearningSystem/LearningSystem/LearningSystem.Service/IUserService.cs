namespace LearningSystem.Service
{
    using LearningSystem.Service.Models;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);
    }
}
