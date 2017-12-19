﻿// <auto-generated />
using HealthR.Data;
using HealthR.Data.Models.Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace HealthR.Data.Migrations
{
    [DbContext(typeof(HealthRDbContext))]
    [Migration("20171219224617_UserScheduleNullable")]
    partial class UserScheduleNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HealthR.Data.Models.Medical.MedicalSheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DoctorId");

                    b.Property<string>("ExaminationDescription")
                        .IsRequired();

                    b.Property<string>("PatientId")
                        .IsRequired();

                    b.Property<int>("PrescriptionId");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("MedicalSheets");
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.Medicament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DoctorId")
                        .IsRequired();

                    b.Property<DateTime>("IssueDate");

                    b.Property<bool>("MultipleUse");

                    b.Property<string>("PatientId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.PrescriptionMedicament", b =>
                {
                    b.Property<int>("PrescriptionId");

                    b.Property<int>("MedicamentId");

                    b.HasKey("PrescriptionId", "MedicamentId");

                    b.HasIndex("MedicamentId");

                    b.ToTable("PrescriptionMedicaments");
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.ToTable("Appointments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Appointment");
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.AppointmentLabel", b =>
                {
                    b.Property<int>("AppointmentId");

                    b.Property<int>("LabelId");

                    b.HasKey("AppointmentId", "LabelId");

                    b.HasIndex("LabelId");

                    b.ToTable("AppointmentLabel");
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Color");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.ScheduleAppointment", b =>
                {
                    b.Property<int>("ScheduleId");

                    b.Property<int>("AppointmentId");

                    b.HasKey("ScheduleId", "AppointmentId");

                    b.HasIndex("AppointmentId");

                    b.ToTable("ScheduleAppointments");
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.ScheduleAppointmentRequest", b =>
                {
                    b.Property<int>("RequestedScheduleId");

                    b.Property<int>("AppointmentRequestId");

                    b.Property<bool>("Confirm");

                    b.HasKey("RequestedScheduleId", "AppointmentRequestId");

                    b.HasIndex("AppointmentRequestId");

                    b.ToTable("ScheduleAppointmentRequest");
                });

            modelBuilder.Entity("HealthR.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int?>("ScheduleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.RecurrenceAppointment", b =>
                {
                    b.HasBaseType("HealthR.Data.Models.Scheduler.Appointment");

                    b.Property<int>("DayOfWeekMask");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("Occurrences");

                    b.Property<int>("RecurrenceEndType");

                    b.Property<int>("RecurrenceType");

                    b.Property<DateTime>("StartDate");

                    b.ToTable("RecurrenceAppointment");

                    b.HasDiscriminator().HasValue("RecurrenceAppointment");
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.Doctor", b =>
                {
                    b.HasBaseType("HealthR.Data.Models.User");


                    b.ToTable("Doctor");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.Patient", b =>
                {
                    b.HasBaseType("HealthR.Data.Models.User");

                    b.Property<string>("DoctorId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patient");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.MedicalSheet", b =>
                {
                    b.HasOne("HealthR.Data.Models.Medical.Doctor", "Doctor")
                        .WithMany("PatientMedicalSheets")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HealthR.Data.Models.Medical.Patient", "Patient")
                        .WithMany("MedicalSheets")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HealthR.Data.Models.Medical.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.Prescription", b =>
                {
                    b.HasOne("HealthR.Data.Models.Medical.Doctor", "Doctor")
                        .WithMany("PatientPrescriptions")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HealthR.Data.Models.Medical.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.PrescriptionMedicament", b =>
                {
                    b.HasOne("HealthR.Data.Models.Medical.Medicament", "Medicament")
                        .WithMany("Prescriptions")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HealthR.Data.Models.Medical.Prescription", "Prescription")
                        .WithMany("Medicaments")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.AppointmentLabel", b =>
                {
                    b.HasOne("HealthR.Data.Models.Scheduler.Appointment", "Appointment")
                        .WithMany("Labels")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HealthR.Data.Models.Scheduler.Label", "Label")
                        .WithMany("Appointments")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.Schedule", b =>
                {
                    b.HasOne("HealthR.Data.Models.User", "Owner")
                        .WithOne("Schedule")
                        .HasForeignKey("HealthR.Data.Models.Scheduler.Schedule", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.ScheduleAppointment", b =>
                {
                    b.HasOne("HealthR.Data.Models.Scheduler.Appointment", "Appointment")
                        .WithMany("Schedules")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HealthR.Data.Models.Scheduler.Schedule", "Schedule")
                        .WithMany("Appointments")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HealthR.Data.Models.Scheduler.ScheduleAppointmentRequest", b =>
                {
                    b.HasOne("HealthR.Data.Models.Scheduler.Appointment", "AppointmentRequest")
                        .WithMany("RequestedSchedules")
                        .HasForeignKey("AppointmentRequestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HealthR.Data.Models.Scheduler.Schedule", "RequestedSchedule")
                        .WithMany("AppointmentRequests")
                        .HasForeignKey("RequestedScheduleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HealthR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HealthR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HealthR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HealthR.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HealthR.Data.Models.Medical.Patient", b =>
                {
                    b.HasOne("HealthR.Data.Models.Medical.Doctor", "Doctor")
                        .WithMany("Patients")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
