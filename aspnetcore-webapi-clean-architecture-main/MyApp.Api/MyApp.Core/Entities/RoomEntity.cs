using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class RoomEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }

        // Foreign Key for Hotel (M:1 relationship)
        public int HotelId { get; set; }
        public HotelEntity Hotel { get; set; }

        //  (M:M relationship)
        public ICollection<ReservationEntity> Reservations { get; set; }
    }

}
