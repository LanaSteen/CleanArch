using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Room
{
    public class UpdateRoomRequest
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
    }
}
