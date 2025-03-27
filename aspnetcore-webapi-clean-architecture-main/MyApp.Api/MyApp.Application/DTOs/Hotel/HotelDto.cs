using MyApp.Application.DTOs.Reservation;
using MyApp.Application.DTOs.Room;

public class HotelDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Rating { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public int? ManagerId { get; set; }
    public string? ManagerName { get; set; }
    public ICollection<RoomDto>? Rooms { get; set; }
    //public ICollection<ReservationDto>? Reservations { get; set; }
}