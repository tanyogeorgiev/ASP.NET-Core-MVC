using System.ComponentModel.DataAnnotations;

namespace HealthR.Web.Areas.Admin.ViewModels
{
    public class AddToRoleFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
