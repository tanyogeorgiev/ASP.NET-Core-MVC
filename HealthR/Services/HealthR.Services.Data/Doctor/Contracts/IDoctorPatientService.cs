using HealthR.Services.Data.Doctor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthR.Services.Data.Doctor.Contracts
{
    public interface IDoctorPatientService
    {
        Task<bool> AddNewPatient(string doctorId, string patientId);

        Task AddNewPrescription( );

        Task AddMedicalSheet();

        Task<IEnumerable<DoctorPatientServiceModel>> GetAllPatients(string doctorId);

        
        Task<IList<DoctorPatientAutocompleteServiceModel>> FindByPrefix(string prefix, string doctorId);


        Task<IList<DoctorPatientAutocompleteServiceModel>> FindByPrefixForSchedule(string prefix, string doctorId);

        Task<DoctorPatientDetailsServiceModel> GetPatientDetails(string patientId);

        Task<bool> DeletePatient(string doctorId,string patientId);

    }
}
