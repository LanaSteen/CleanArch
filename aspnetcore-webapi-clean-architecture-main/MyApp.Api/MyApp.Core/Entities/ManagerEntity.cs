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
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? HotelId { get; set; } // Assuming HotelId is an int or nullable int
        public HotelEntity Hotel { get; set; } // Navigation property to Hotel
        public string UserId { get; set; } // Foreign key to UserEntity
        public UserEntity User { get; set; } // Navigation property to UserEntity
    }


}
