using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs
{
    public class CreateAdminDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [PasswordValidation]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [RoleValidation]
        public string Role { get; set; }
    }
}