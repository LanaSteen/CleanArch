using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs
{
    public class UpdateAdminDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [PasswordValidation]
        public string PasswordHash { get; set; }
    }
}