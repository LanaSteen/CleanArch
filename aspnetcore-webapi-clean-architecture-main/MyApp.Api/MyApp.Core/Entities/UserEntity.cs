using Microsoft.AspNetCore.Identity;

namespace MyApp.Core.Entities
{
    public class UserEntity : IdentityUser
    {
        public ICollection<ReservationEntity> Reservations { get; set; }

        public string Role { get; set; } = string.Empty;
    }
}
