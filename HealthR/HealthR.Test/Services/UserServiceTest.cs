namespace HealthR.Test.Services
{
    using FluentAssertions;
    using HealthR.Data;
    using HealthR.Data.Models;
    using HealthR.Data.Models.Scheduler;
    using HealthR.Services.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServiceTest
    {
       

        [Fact]
        public async Task CreateScheduleShouldChangeUserScheduleAndMakeOldInactive()
        {
            // Arrange
            var db = this.GetDatabase();
           
            var userScheduleId = 10;
            var userId = "88";
            var currentSchedule = new Schedule()
            {
                Id = 10,
                OwnerId = userId,
                IsActive = true
            };


            var user = new User()
            {
                Id = userId,
                ScheduleId = userScheduleId
            };

            db.Add(user);
            db.Add(currentSchedule);

            await db.SaveChangesAsync();
            var userService = new UserService(db);

            // Act

            await userService.CreateSchedule(userId, "New Schedule");

            // Assert
            var userCheck = db.Users.Find(userId);

            var userSchedulesCount = userCheck.Schedules.Count;
            var scheduleName = db.Schedules.Where(s => s.Id == userCheck.ScheduleId).Select(n =>n.Name).FirstOrDefault();
            var oldScheduleActive = db.Schedules.Where(s => s.Id == userScheduleId).Select(o => o.IsActive).FirstOrDefault();

            userSchedulesCount.Should().Be(2);
            scheduleName.ShouldAllBeEquivalentTo("New Schedule");
            oldScheduleActive.ShouldBeEquivalentTo(false);
            

        }
        private HealthRDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<HealthRDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new HealthRDbContext(dbOptions);
        }
    }
    }
