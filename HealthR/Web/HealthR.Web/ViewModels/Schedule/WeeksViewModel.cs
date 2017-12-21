namespace HealthR.Web.ViewModels.Schedule
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WeeksViewModel
    {
        public DateTime FirstDayOfWeek
        { get; set; }

        [Display(Name = "Number of week")]
        public int WeekNumber { get; set; }
    }
}
