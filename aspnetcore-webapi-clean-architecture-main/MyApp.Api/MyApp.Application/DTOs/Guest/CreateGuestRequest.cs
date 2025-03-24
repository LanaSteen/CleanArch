using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Guest
{
    public class CreateGuestRequest
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Personal Number is required.")]
        public string PersonalNumber { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }  
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [PasswordValidation]
        public string Password { get; set; }


    }
}
