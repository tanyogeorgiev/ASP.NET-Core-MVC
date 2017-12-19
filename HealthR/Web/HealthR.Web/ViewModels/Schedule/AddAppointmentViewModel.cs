
namespace HealthR.Web.ViewModels.Schedule
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AppointmentViewModel
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public string StartDate { get; set; }

        public DateTime FirstDayOfWeek { get; set; }

        [Display(Name ="Number of week")]
        public int WeekNumber { get; set; }


    }
}
