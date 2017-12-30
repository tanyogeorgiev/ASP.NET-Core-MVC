
namespace HealthR.Web.ViewModels.Users
{
    using HealthR.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class CreateUserScheduleViewModel
    {
        [Required]
        [MaxLength(DataConstants.ScheduleNameMaxLength)]
        public string Name { get; set; }

        public string OwnerId { get; set; }
    }
}
