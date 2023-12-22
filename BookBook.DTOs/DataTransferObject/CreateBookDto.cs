using System.ComponentModel.DataAnnotations;

namespace BookBook.DTOs;

public record CreateBookDto : IValidatableObject
{

    // [PhoneNumberInputValid(validInputs: "Book valid 1, Book valid 2, Book valid 3", ErrorMessage = $"Please enter a valid book name from the following list: Book valid 1, Book valid 2, Book valid 3")]
    public string BookName { get; set; }
    public DateTime CreatedDate { get; set; }
    [Range(10, int.MaxValue)]
    public int Price { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validInputList = new string[] {"Book valid 1", "Book valid 2", "Book valid 3"};
        if (!validInputList.Contains(BookName))
         {
             var errorMessage = $"Please enter a valid book name from the following list: Book valid 1, Book valid 2, Book valid 3";
             yield return new ValidationResult(errorMessage, new[] {nameof(BookName)});
         }

    }

}
