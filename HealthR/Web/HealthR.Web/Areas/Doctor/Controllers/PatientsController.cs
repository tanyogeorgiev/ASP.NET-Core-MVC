

namespace HealthR.Web.Areas.Doctor.Controllers
{
    using HealthR.Data.Models;
    using HealthR.Services.Data.Doctor.Contracts;
    using HealthR.Web.Areas.Doctor.Models;
    using HealthR.Web.Infrastructure.Exstensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PatientsController : BaseDoctorController
    {
        private readonly IDoctorPatientService patients;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public PatientsController(
            IDoctorPatientService patients,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.patients = patients;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var doctorId = this.userManager.GetUserId(User);

            var result = await this.patients.GetAllPatients(doctorId);

            return View(result);
        }

        public async Task<IActionResult> AddNewPatient(string id)
        {
            var patient = await this.userManager.FindByIdAsync(id);

            var doctorId = this.userManager.GetUserId(User);

            if (patient == null)
            {
                return BadRequest();
            }

            var result = await this.patients.AddNewPatient(doctorId, patient.Id);
            TempData.AddSuccessMessage($"User {patient.UserName} syccessfully added as your Patient.");
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<JsonResult> AutoComplete([FromBody] DoctorAutocompletePrefixViewModel model)
        {

            var doctorId = this.userManager.GetUserId(User);


            var patients = await this.patients
                .FindByPrefix(model.Prefix, doctorId);

            return Json(patients);
        }

        [HttpPost]
        public async Task<JsonResult> AutoCompletePatients([FromBody] DoctorAutocompletePrefixViewModel model)
        {

            var doctorId = this.userManager.GetUserId(User);


            var patients = await this.patients
                .FindByPrefixForSchedule(model.Prefix, doctorId);

            return Json(patients);
        }

        public async Task<JsonResult> GetPatientDetails(string id)
        {
            var patient = await this.patients.GetPatientDetails(id);
            return Json(patient);
        }

        

        public async Task<IActionResult> DeletePatient(string id)
        {
            var patient = await this.userManager.FindByIdAsync(id);

            var doctorId = this.userManager.GetUserId(User);

            if (patient == null)
            {
                return BadRequest();
            }

            var result = await this.patients.DeletePatient(doctorId, patient.Id);

            if (!result)
            {
                TempData.AddErrorMessage($"User {patient.UserName} not deleted from your Patient.");

            }
            else
            {
                TempData.AddSuccessMessage($"User {patient.UserName} syccessfully deleted from your Patient.");
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
