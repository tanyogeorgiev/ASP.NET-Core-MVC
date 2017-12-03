namespace BookShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.AuthorNameMinLength)]
        [MaxLength(DataConstants.AuthorNameMaxLength)]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(DataConstants.AuthorNameMinLength)]
        [MaxLength(DataConstants.AuthorNameMaxLength)]
        public string LastName { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();




    }
}
