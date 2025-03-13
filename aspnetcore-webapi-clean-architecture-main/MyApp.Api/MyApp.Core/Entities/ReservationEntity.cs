using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class ReservationEntity
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        // Foreign Keys
        public int GuestId { get; set; }
        public GuestEntity Guest { get; set; } = null!;

        public int RoomId { get; set; }
        public RoomEntity Room { get; set; } = null!;
    }

}
