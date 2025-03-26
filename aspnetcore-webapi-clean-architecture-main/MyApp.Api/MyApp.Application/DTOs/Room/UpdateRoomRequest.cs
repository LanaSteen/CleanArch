using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Room
{
    public class UpdateRoomRequest
    {
        public string? Name { get; set; }
        public bool? IsAvailable { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }

        public int? HotelId { get; set; } 
    }
}
