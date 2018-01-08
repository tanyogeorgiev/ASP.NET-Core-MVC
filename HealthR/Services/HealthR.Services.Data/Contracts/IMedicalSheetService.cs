
namespace HealthR.Services.Data.Contracts
{
    using HealthR.Services.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMedicalSheetService
    {
        Task<IEnumerable<MedicalSheetServiceModel>> All(string userId);
    }
}
