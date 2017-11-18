
namespace CarDealer.Web.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Display(Name="Is young driver?")]
        public bool IsYoungDriver { get; set; }
    }
}
