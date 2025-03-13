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
        public string Name { get; set; } = string.Empty;
        public int Rating { get; set; } // Range 1-5
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Relationships
        public ManagerEntity? Manager { get; set; }
        public ICollection<RoomEntity> Rooms { get; set; } = new List<RoomEntity>();
        public ICollection<ReservationEntity> Reservations { get; set; } = new List<ReservationEntity>();
    }
}
