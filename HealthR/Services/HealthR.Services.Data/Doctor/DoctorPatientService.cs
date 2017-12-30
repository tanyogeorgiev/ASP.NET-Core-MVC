
namespace HealthR.Services.Data.Doctor
{
    using System.Threading.Tasks;
    using HealthR.Data;
    using HealthR.Services.Data.Doctor.Contracts;
    using HealthR.Services.Data.Doctor.Models;
    using HealthR.Data.Models.Medical;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;


    public class DoctorPatientService : IDoctorPatientService
    {

        private readonly HealthRDbContext db;

        public DoctorPatientService(HealthRDbContext db)
        {
            this.db = db;
        }



        public async Task<bool> AddNewPatient(string doctorId, string patientId)
        {
            var doctor = this.db.Users.Find(doctorId);
            var patient = this.db.Users.Find(patientId);


            var doctorPatient = new DoctorPatients
            {
                DoctorId = doctorId,
                PatientId = patientId
            };
            var currentDp = this.db
                .DoctorPatients
                .Where(p => p.PatientId == patientId)
                .FirstOrDefault();

            if (currentDp != null)
            {
                this.db.DoctorPatients.Remove(currentDp);
            }

            this.db.DoctorPatients.Add(doctorPatient);

            patient.DoctorId = doctor.Id;

            await this.db.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<DoctorPatientServiceModel>> GetAllPatients(string doctorId)
        => await this.db
                 .Users
                 .Where(u => u.Id == doctorId)
                 .SelectMany(d => d.Patients.Select(p => p.Patient))
                 .ProjectTo<DoctorPatientServiceModel>()
                 .ToListAsync();


        public async Task<DoctorPatientDetailsServiceModel> GetPatientDetails(string patientId)
            => await this.db
            .Users
            .Where(u => u.Id == patientId)
            .ProjectTo<DoctorPatientDetailsServiceModel>()
            .FirstOrDefaultAsync();



        public async Task<IList<DoctorPatientAutocompleteServiceModel>> FindByPrefix(string prefix, string doctorId)
        {
            var userPatients = await this.db.Users
                .Where(u => u.Id == doctorId)
                .Select(p => new
                {
                    Patients = p.Patients.Select(s => s.PatientId).ToList()
                })
                .FirstOrDefaultAsync();

            var allUsers = await this.db.Users
            .Where(p => p.Name.Contains(prefix) && !userPatients.Patients.Contains(p.Id))
            .Take(15)
            .ProjectTo<DoctorPatientAutocompleteServiceModel>()
            .ToListAsync();


            return allUsers;
        }

        public async Task<bool> DeletePatient(string doctorId, string patientId)
        {
            var doctor = this.db.Users.Find(doctorId);
            var patient = this.db.Users.Find(patientId);

            var doctorPatient = this.db.Find<DoctorPatients>(doctorId, patientId);

            if (doctorPatient == null)
            {
                return false;
            }
            this.db.Remove<DoctorPatients>(doctorPatient);
            await this.db.SaveChangesAsync();
            return true;


        }


        public Task AddMedicalSheet()
        {
            throw new System.NotImplementedException();
        }

        public Task AddNewPrescription()
        {
            throw new System.NotImplementedException();
        }
    }
}
