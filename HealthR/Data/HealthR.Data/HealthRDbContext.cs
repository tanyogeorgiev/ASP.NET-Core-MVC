

namespace HealthR.Data
{
    using Data.Models;
    using Data.Models.Scheduler;
    using Data.Models.Medical;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    

    public class HealthRDbContext : IdentityDbContext<User>
    {
        public HealthRDbContext(DbContextOptions<HealthRDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalSheet> MedicalSheets { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }


        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleAppointment> ScheduleAppointments { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<RecurrenceAppointment> RecurrenceAppointments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ScheduleAppointment>()
                .HasKey(sa => new { sa.ScheduleId, sa.AppointmentId });

            builder
                .Entity<ScheduleAppointment>()
                .HasOne(sa => sa.Schedule)
                .WithMany(s => s.Appointments)
                .HasForeignKey(fk => fk.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .Entity<ScheduleAppointment>()
                .HasOne(sa => sa.Appointment)
                .WithMany(a => a.Schedules)
                .HasForeignKey(fk => fk.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ScheduleAppointmentRequest>()
                .HasKey(pk => new { pk.RequestedScheduleId, pk.AppointmentRequestId });

            builder
                .Entity<ScheduleAppointmentRequest>()
                .HasOne(sa => sa.RequestedSchedule)
                .WithMany(s => s.AppointmentRequests)
                .HasForeignKey(fk => fk.RequestedScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ScheduleAppointmentRequest>()
                .HasOne(sa => sa.AppointmentRequest)
                .WithMany(a => a.RequestedSchedules)
                .HasForeignKey(fk => fk.AppointmentRequestId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<AppointmentLabel>()
                .HasKey(al => new { al.AppointmentId, al.LabelId });

            builder
                .Entity<AppointmentLabel>()
                .HasOne(al => al.Appointment)
                .WithMany(a => a.Labels)
                .HasForeignKey(fk => fk.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .Entity<AppointmentLabel>()
                .HasOne(al => al.Label)
                .WithMany(l => l.Appointments)
                .HasForeignKey(fk => fk.LabelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(fk => fk.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.PatientPrescriptions)
                .HasForeignKey(fk => fk.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Patient>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(fk => fk.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

          

            builder
                .Entity<MedicalSheet>()
                .HasOne(ms => ms.Patient)
                .WithMany(p => p.MedicalSheets)
                .HasForeignKey(fk => fk.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MedicalSheet>()
                .HasOne(ms => ms.Doctor)
                .WithMany(d => d.PatientMedicalSheets)
                .HasForeignKey(fk => fk.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            
           
            builder
                .Entity<PrescriptionMedicament>()
                .HasKey(pm => new { pm.PrescriptionId, pm.MedicamentId });
                

            builder
                .Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.Medicaments)
                .HasForeignKey(fk => fk.PrescriptionId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(fk => fk.MedicamentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<User>()
                .HasOne(u => u.Schedule)
                .WithOne(s => s.Owner)
                .HasForeignKey<Schedule>(b=>b.OwnerId)
                ;

            base.OnModelCreating(builder);
        
        }
    }
}
