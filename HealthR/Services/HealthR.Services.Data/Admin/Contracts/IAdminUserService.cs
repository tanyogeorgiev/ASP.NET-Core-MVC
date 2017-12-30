
namespace HealthR.Services.Data.Admin.Contracts
{
    using HealthR.Services.Data.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserServiceModel>> AllAsync();
    }
}
