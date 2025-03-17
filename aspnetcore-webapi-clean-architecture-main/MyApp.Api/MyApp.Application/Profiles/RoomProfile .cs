using AutoMapper;
using MyApp.Application.DTOs.Reservation;
using MyApp.Application.DTOs.Room;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            // Mapping RoomEntity to RoomDto
            CreateMap<RoomEntity, RoomDto>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));

            // Mapping ReservationEntity to ReservationDto
            CreateMap<ReservationEntity, ReservationDto>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"));

            // Add mapping for CreateRoomRequest -> RoomEntity
            CreateMap<CreateRoomRequest, RoomEntity>();
            CreateMap<UpdateRoomRequest, RoomEntity>();
        }
    }
    

}
