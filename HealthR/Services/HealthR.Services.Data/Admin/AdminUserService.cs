
namespace HealthR.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using HealthR.Data;
    using HealthR.Services.Data.Admin.Contracts;
    using HealthR.Services.Data.Admin.Models;
    using Microsoft.EntityFrameworkCore;

    public class AdminUserService : IAdminUserService
    {
       

        private readonly HealthRDbContext db;

        public AdminUserService(HealthRDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserServiceModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserServiceModel>()
                .ToListAsync();
    }
}

