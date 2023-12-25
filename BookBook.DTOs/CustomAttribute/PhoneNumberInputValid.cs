using System.ComponentModel.DataAnnotations;

namespace BookBook.DTOs;

public class PhoneNumberInputValid : ValidationAttribute
{
    private readonly string validInputs;
   public PhoneNumberInputValid(string validInputs)
   {
      this.validInputs = validInputs;
   }
   protected override ValidationResult? IsValid(object value,     ValidationContext validationContext)
   {
      var validInputList = this.validInputs.Split(",");
 
      if (!validInputList.Contains(value))
         return new ValidationResult(this.ErrorMessage, new List<string>()  { validationContext.MemberName });
      return ValidationResult.Success;
   }

}
