
namespace HealthR.Web.Areas.Doctor.Controllers
{
    using AutoMapper;
    using HealthR.Data.Models;
    using HealthR.Services.Data.Doctor.Contracts;
    using HealthR.Web.Areas.Doctor.Models;
    using HealthR.Web.Controllers;
    using HealthR.Web.Infrastructure.Exstensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MedicalSheetController : BaseDoctorController
    {
        private readonly IDoctorPatientService patients;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IDoctorMedicalSheetService medicalSheets;


        public MedicalSheetController(
            IDoctorPatientService patients,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IDoctorMedicalSheetService medicalSheets)
        {
            this.patients = patients;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.medicalSheets = medicalSheets;
        }

      
        public async Task<IActionResult> New(string patientId )
        {
            var patient = await this.patients.GetPatientDetails(patientId);
            var doctorId = this.userManager.GetUserId(User);
            var doctor = await this.patients.GetPatientDetails(doctorId);

            var model = new DoctorMedicalSheetViewModel()
            {
                Patient = patient,
                Doctor = doctor
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> New(DoctorMedicalSheetFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var doctor =await this.userManager.FindByIdAsync(model.DoctorId);
            var patient =await this.userManager.FindByIdAsync(model.PatientId);

            if(doctor == null || patient == null)
            {
                return BadRequest();
            }

            await this.medicalSheets.AddMedicalSheet(
                model.DoctorId,
                model.PatientId,
                model.ExaminationDescription);


            this.TempData.AddSuccessMessage(WebConstants.MedicalSheetSuccessMessage);
            return RedirectToAction(nameof(ScheduleController.ByWeek), "Schedule", new { area= string.Empty });
        }

        public async Task<IActionResult> All()
        {
            var userId = this.userManager.GetUserId(User);

            var result = await this.medicalSheets.All(userId);

            return View(result);
        }

    }
}
