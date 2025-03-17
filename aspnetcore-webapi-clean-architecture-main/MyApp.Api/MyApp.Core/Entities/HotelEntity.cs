using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class HotelEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }  // Rating between 1 and 5
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        // Navigation property for Manager (1:1 relationship)
        public int ManagerId { get; set; }
        public ManagerEntity Manager { get; set; }

        // Navigation property for Rooms (1:M relationship)
        public ICollection<RoomEntity> Rooms { get; set; }

        // Navigation property for Reservations (M:M relationship)
        public ICollection<ReservationEntity> Reservations { get; set; }
    }
}
