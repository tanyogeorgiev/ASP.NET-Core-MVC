
namespace HealthR.Services.Data
{
    using AutoMapper.QueryableExtensions;
    using HealthR.Data;
    using HealthR.Data.Models.Scheduler;
    using HealthR.Services.Data.Contracts;
    using HealthR.Services.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly HealthRDbContext db;
        // private readonly IPdfGenerator pdfGenerator;

        public UserService(HealthRDbContext db)
        {
            this.db = db;

        }

        public async Task CreateSchedule(string userId, string name)
        {
            var schedule = new Schedule
            {
                Name = name,
                OwnerId = userId
            };

            var oldschedule = this.db.Schedules.Where(s => s.OwnerId == userId && !s.IsDeleted).FirstOrDefault();
            if (oldschedule != null)
            {
                oldschedule.IsDeleted = true;
            }

            this.db.Schedules.Add(schedule);

            this.db.SaveChanges();

            var user = this.db.Users.Find(userId);
            user.ScheduleId = schedule.Id;

            await this.db.SaveChangesAsync();


        }

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
         => await this.db
             .Users
             .Where(u => u.Id == id)
             .ProjectTo<UserProfileServiceModel>()
             .FirstOrDefaultAsync();


    }
}
