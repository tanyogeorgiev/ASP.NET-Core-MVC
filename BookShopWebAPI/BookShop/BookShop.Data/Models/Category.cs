using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.CategoryNameMinLength)]
        [MaxLength(DataConstants.CategoryNameMaxLength)]
        public string Name { get; set; }

        public List<BookCategory> Books { get; set; } = new List<BookCategory>();
    }
}
