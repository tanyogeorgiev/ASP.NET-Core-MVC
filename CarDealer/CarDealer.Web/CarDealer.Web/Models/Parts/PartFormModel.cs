
namespace CarDealer.Web.Models.Parts
{
    using System.ComponentModel.DataAnnotations;
    using Services.Models.Suppliers;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PartFormModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0.0, double.MaxValue)]
        
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> SuppliersList { get; set; }

    }
}
