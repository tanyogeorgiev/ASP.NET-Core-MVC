
namespace HealthR.Web.Controllers
{
    using HealthR.Data.Models;
    using HealthR.Services.Data.Contracts;
    using HealthR.Services.Data.Doctor.Contracts;
    using HealthR.Web.ViewModels.MedicalSheet;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MedicalSheetController : BaseController
    {
        private readonly IDoctorPatientService patients;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IMedicalSheetService medicalSheets;


        public MedicalSheetController(
            IDoctorPatientService patients,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IMedicalSheetService medicalSheets)
        {
            this.patients = patients;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.medicalSheets = medicalSheets;
        }

        public async Task<IActionResult> All()
        {
            var userId = this.userManager.GetUserId(User);

            var result = await this.medicalSheets.All(userId);

            return View(result);
        }
        

    }
}
