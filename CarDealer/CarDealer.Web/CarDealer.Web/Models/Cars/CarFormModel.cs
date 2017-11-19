
namespace CarDealer.Web.Models.Cars
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Make { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Range(0, long.MaxValue,ErrorMessage ="Travelled distance must be positive number"),Display(Name ="Travelled Distance")]
        public long TravelledDistance { get; set; }

        [Display(Name = "Parts")]
        public int[] PartId { get; set; }

        public IEnumerable<SelectListItem> PartsList { get; set; }

    }
}
