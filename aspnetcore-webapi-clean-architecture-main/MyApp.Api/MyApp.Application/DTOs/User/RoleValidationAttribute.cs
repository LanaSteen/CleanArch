using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs
{
    public class RoleValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var role = value as string;
            if (string.IsNullOrEmpty(role))
            {
                return new ValidationResult("Role is required.");
            }

            if (role != "Admin")
            {
                return new ValidationResult("Role must be 'Admin'.");
            }

            return ValidationResult.Success;
        }
    }
}