﻿using System;
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
        public int HotelId { get; set; }
        public int RoomId { get; set; }


        public HotelEntity Hotel { get; set; }
        public RoomEntity Room { get; set; }
        public string GuestId { get; set; } // Change this to string to match UserEntity's Id
        public UserEntity Guest { get; set; }
    }

}
