using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Hotel
{
    public class UpdateHotelRequest
    {
        
        public string? Name { get; set; }
        public int? Rating { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
    }
}
