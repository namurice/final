using System.ComponentModel.DataAnnotations;

namespace FinalsProject.Validations
{
    public class Phone : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Phone number is required");
            }
            if (value is string phone)
            {
                long number;
                if (long.TryParse(phone, out number))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid");
                }
            }
            else
            {
                return new ValidationResult("Invalid");
            }
        }
    }
}
