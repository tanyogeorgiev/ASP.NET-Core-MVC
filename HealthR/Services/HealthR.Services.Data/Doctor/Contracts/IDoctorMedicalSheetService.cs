
namespace HealthR.Services.Data.Doctor.Contracts
{
    using HealthR.Services.Data.Doctor.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDoctorMedicalSheetService
    {

         Task AddMedicalSheet(
             string doctroId,
             string patientId,
             string examination);

        Task<IEnumerable<DoctorMedicalSheetServiceModel>> All(string userId);


    }
}
