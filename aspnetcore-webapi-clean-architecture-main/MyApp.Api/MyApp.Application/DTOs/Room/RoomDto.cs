using MyApp.Application.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Room
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public string HotelName { get; set; }  // Hotel Name mapped from Hotel entity
        public List<ReservationDto> Reservations { get; set; }  // Reservations mapped from Reservation entity
    }
}
