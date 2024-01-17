using System.ComponentModel.DataAnnotations;

namespace FinalsProject.Validations
{
    public class EmailAddress : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Email address is required");
            }
            if (value is string email) 
            {
                if (email.Contains('@') && email.Contains('.'))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid");
                }
            }
            return new ValidationResult("Email Address must be a string");
        }
    }
}
