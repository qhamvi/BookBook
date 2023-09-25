using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBook.Models.Models
{
    [Table("authors")]
    public class Author
    {
        [Column("AuthorId")]
        public Guid Id {get; set;}

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName {get; set;}

        [Required(ErrorMessage = "LastName is required")]
        public string LastName {get; set;}
        public string PhoneNumber {get; set;}

        [Required(ErrorMessage = "DayOfBirth is required")]
        public DateTime DayOfBirth {get; set;}
        public string Country {get; set;}
        public ICollection<Book>? Books {get; set;}
    }
}