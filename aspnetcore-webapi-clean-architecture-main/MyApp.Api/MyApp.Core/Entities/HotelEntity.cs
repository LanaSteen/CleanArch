using MyApp.Core.Entities;

public class HotelEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }  // 1 -5 რეიტინგი
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }

    public int ManagerId { get; set; }
    public ManagerEntity Manager { get; set; }

    // Navigation property for Rooms (1:M relationship)
    public ICollection<RoomEntity> Rooms { get; set; }

    // Navigation property for Reservations (M:M relationship)
    public ICollection<ReservationEntity> Reservations { get; set; }
}