using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Guest
{
    public class GuestDto
    {
        public string? Id { get; set; }  
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
