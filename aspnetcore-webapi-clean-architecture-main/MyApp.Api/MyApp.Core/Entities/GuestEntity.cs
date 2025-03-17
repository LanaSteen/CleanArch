using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class GuestEntity
    {
        public int Id { get; set; }  // Primary Key (Identity)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }  // Personal ID
        public string PhoneNumber { get; set; }

        // Navigation property for Reservations (M:M relationship)
        public ICollection<ReservationEntity> Reservations
        {
            get; set;
        }
    }
}
