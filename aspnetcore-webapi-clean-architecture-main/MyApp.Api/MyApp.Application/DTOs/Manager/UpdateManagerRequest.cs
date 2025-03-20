using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Manager
{
    public class UpdateManagerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int HotelId { get; set; }
        public string Password { get; set; }
    }
}
