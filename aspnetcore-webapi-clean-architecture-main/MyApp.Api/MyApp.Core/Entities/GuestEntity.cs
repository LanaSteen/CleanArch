using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
  
        public class GuestEntity : UserEntity  // Now inherits from UserEntity
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PersonalNumber { get; set; }
            public string PhoneNumber { get; set; }

            // Navigation property for Reservations (M:M relationship)
            public ICollection<ReservationEntity> Reservations { get; set; }
    }


    
}
