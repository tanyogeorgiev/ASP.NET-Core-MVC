

namespace HealthR.Services.Data
{
    using AutoMapper.QueryableExtensions;
    using HealthR.Data;
    using HealthR.Services.Data.Contracts;
    using HealthR.Services.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MedicalSheetService : IMedicalSheetService
    {
        private readonly HealthRDbContext db;

        public MedicalSheetService(HealthRDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<MedicalSheetServiceModel>> All(string userId)
        => await this.db.MedicalSheets.Where(ms => ms.PatientId == userId).ProjectTo<MedicalSheetServiceModel>().ToListAsync();
            
    }
}
