﻿using System;
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
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string GuestId { get; set; } // Changed from int to string
        public string GuestEmail { get; set; } // Useful to include
        public string RoomName { get; set; } // Useful to include
        public string HotelName { get; set; } // Useful to include
    }
}
