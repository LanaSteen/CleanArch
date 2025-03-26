using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class ManagerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? HotelId { get; set; } 
        public HotelEntity Hotel { get; set; } 
        public string UserId { get; set; } 
        public UserEntity User { get; set; }
    }


}
