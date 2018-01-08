
namespace HealthR.Services.Data.Doctor
{
    using AutoMapper.QueryableExtensions;
    using HealthR.Data;
    using HealthR.Data.Models.Medical;
    using HealthR.Services.Data.Doctor.Contracts;
    using HealthR.Services.Data.Doctor.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DoctorMedicalSheetService : IDoctorMedicalSheetService
    {

        private readonly HealthRDbContext db;

        public DoctorMedicalSheetService(HealthRDbContext db)
        {
            this.db = db;
        }

        public async Task AddMedicalSheet(string doctroId, string patientId, string examination)
        {
            var medicalSheet = new MedicalSheet()
            {
                PatientId = patientId,
                DoctorId = doctroId,
                ExaminationDescription = examination
            };

            this.db.Add(medicalSheet);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorMedicalSheetServiceModel>> All(string userId)
        => await this.db.MedicalSheets.Where(ms => ms.DoctorId == userId).ProjectTo<DoctorMedicalSheetServiceModel>().ToListAsync();
    }
}
