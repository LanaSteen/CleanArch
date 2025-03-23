using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyApp.Application.DTOs
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required.");
            }

            // პაროლის ვალიდაცია
            var hasMinimumLength = password.Length >= 8;
            var hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            var hasLowerCase = Regex.IsMatch(password, "[a-z]");
            var hasDigit = Regex.IsMatch(password, "[0-9]");

            if (!hasMinimumLength || !hasUpperCase || !hasLowerCase || !hasDigit)
            {
                return new ValidationResult("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.");
            }

            return ValidationResult.Success;
        }
    }
}