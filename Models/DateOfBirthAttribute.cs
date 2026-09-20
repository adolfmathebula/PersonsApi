using System.ComponentModel.DataAnnotations;

namespace PersonsAPI.Models
{

    public class DateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Date of birth is required.");
            }

            if (value is DateTime dateOfBirth)
            {
                if (dateOfBirth.Date > DateTime.Today)
                {
                    return new ValidationResult(
                        "Date of birth cannot be in the future.");
                }
            }

            return ValidationResult.Success;
        }
    }
}