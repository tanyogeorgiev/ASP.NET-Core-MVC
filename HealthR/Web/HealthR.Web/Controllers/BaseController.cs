
namespace HealthR.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : Controller
    {
        public IActionResult ViewOrNotFound( object model)
        {
            
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }
    }
}
