namespace HealthR.Test.Services
{
    using FluentAssertions;
    using HealthR.Data;
    using HealthR.Data.Models;
    using HealthR.Data.Models.Scheduler;
    using HealthR.Services.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class AppointmentServiceTest
    {
        public AppointmentServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task DeleteByIdCorectlyForOwnAppointment()
        {
            // Arrange
            var db = this.GetDatabase();
            var firstAppointment = new Appointment()
            {
                Id = 1,
                CreatorId = "99",
                Title = "Title"
            };

            db.Add(firstAppointment);

            await db.SaveChangesAsync();

            var appointmentService = new AppointmentService(db);

            // Act
             await appointmentService.DeleteById(1, "99");

            // Assert
            var deletedAppointment = db.Appointments.Find(1);
            deletedAppointment.IsDeleted.ShouldBeEquivalentTo(true);
        }

        [Fact]
        public async Task DeleteByIdCorectlyForRequestedAppointment()
        {
            // Arrange
            var db = this.GetDatabase();
            var appointmentId = 1;
            var firstAppointment = new Appointment()
            {
                Id = appointmentId,
                CreatorId = "99",
                Title = "Title"
            };
            var userScheduleId = 10;
            var userId = "88";
            var user = new User()
            {
                Id = userId,
                ScheduleId = userScheduleId
            };

            db.Add(user);
            db.Add(firstAppointment);

            await db.SaveChangesAsync();

            var appointmentService = new AppointmentService(db);

            // Act
            var scheduleAppointment = new ScheduleAppointment()
            {
                ScheduleId = userScheduleId,
                AppointmentId = appointmentId
            };

             db.ScheduleAppointments.Add(scheduleAppointment);

            await appointmentService.DeleteById(appointmentId, userId);

            // Assert
            var deletedAppointment = db.ScheduleAppointments.Find(userScheduleId, appointmentId);
            deletedAppointment.Should().BeNull();
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

