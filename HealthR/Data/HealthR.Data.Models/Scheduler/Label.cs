

namespace HealthR.Data.Models.Scheduler
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Label
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.LabelNameMaxLength)]
        public string Title { get; set; }

        [Required]
        public int Color { get; set; }

        public List<AppointmentLabel> Appointments { get; set; } = new List<AppointmentLabel>();
        
    }
}
