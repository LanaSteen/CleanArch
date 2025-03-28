﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Reservation
{
    public class UpdateReservationRequest
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
        public string? GuestId { get; set; }
    }
}
