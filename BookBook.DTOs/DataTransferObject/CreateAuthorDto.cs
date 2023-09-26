using System.ComponentModel.DataAnnotations;

namespace BookBook.DTOs.DataTransferObject
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50, ErrorMessage = "FirstName can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "LastName can't be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DayOfBirth { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Country cannot be loner then 50 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [StringLength(10, ErrorMessage = "PhoneNumber cannot be loner then 10 characters")]
        public string PhoneNumber { get; set; }
    }
}