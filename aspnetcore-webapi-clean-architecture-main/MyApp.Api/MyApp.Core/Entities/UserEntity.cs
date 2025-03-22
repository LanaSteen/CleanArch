using Microsoft.AspNetCore.Identity;

namespace MyApp.Core.Entities
{
    public class UserEntity : IdentityUser
    {
        public ICollection<ReservationEntity> Reservations { get; set; }  // Add this line

        public string Role { get; set; } = string.Empty;
    }
}
