
namespace CameraStore.Web.Models.Cameras
{
    using Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddCameraViewModel
    {

        public CameraMakeType Make { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }


        public MinISO MinISO { get; set; }

        [Range(200, 409600)]
        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }

        [Required]
        [StringLength(15)]
        public string VideoResolution { get; set; }

        public IEnumerable<LightMetering> LightMetering { get; set; }

        [StringLength(6000)]
        public string Description { get; set; }

        [Required]
        [StringLength(2000,MinimumLength =10)]
        public string ImageURL { get; set; }
    }
}
