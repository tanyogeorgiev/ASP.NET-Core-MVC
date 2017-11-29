

namespace LearningSystem.Service.Admin
{
    using LearningSystem.Service.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
       Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
