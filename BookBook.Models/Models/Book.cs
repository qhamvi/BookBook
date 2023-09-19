using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBook.Models.Models
{
    [Table("books")]
    public class Book
    {
        public Guid BookId {get; set;}
        [Required(ErrorMessage = "BookName is required")]
        public string BookName {get; set;}
        public DateTime CreatedDate {get; set;}
        public int Price {get; set;}

        [ForeignKey(nameof(Book))]
        public Guid AuthorId {get; set;}
        public Author? Author {get; set;}

    }
}