using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Reservation
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomName { get; set; }
        public string GuestName { get; set; }  // You can map it like "Guest.FirstName + ' ' + Guest.LastName"
    }
}
