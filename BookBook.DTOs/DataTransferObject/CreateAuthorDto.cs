using System.ComponentModel.DataAnnotations;

namespace BookBook.DTOs.DataTransferObject
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DayOfBirth { get; set; }
        
        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "Country cannot be loner then 100 characters")]
        public string? Country { get; set; }
    }
}