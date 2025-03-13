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
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public decimal Price { get; set; }

        // Foreign Key
        public int HotelId { get; set; }
        public HotelEntity Hotel  { get; set; } = null!;
    }

}
