
namespace HealthR.Web.Areas.Doctor.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(WebConstants.DoctorArea)]
    [Authorize(Roles = WebConstants.DoctorRole)]
    public class BaseDoctorController : Controller
    {

    }
}
