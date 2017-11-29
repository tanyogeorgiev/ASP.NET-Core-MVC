

namespace LearningSystem.Service
{
    using LearningSystem.Service.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> Active();

        Task<CourseDetailsServiceModel> ByIdAsync(int id);

        Task<bool> UserIsSignedInCourseAsync(int courseId, string userId);

        Task<bool> SignInUserAsync(int courseId, string userId);

        Task<bool> SignOutUserAsync(int id, string userId);
    }
}
