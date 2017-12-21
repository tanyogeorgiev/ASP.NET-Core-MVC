
namespace HealthR.Web.ViewModels.Schedule
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(200)]
        [Required]
        public string Description { get; set; }

        [Required]
        public string StartTime { get; set; }






    }
}
