using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class ManagerEntity
    {
        public int Id { get; set; }  // Primary Key (Identity)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }  // Personal ID
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property for the associated Hotel (1:1 relationship)
        public int HotelId { get; set; }
        public HotelEntity Hotel { get; set; }
    }

}
