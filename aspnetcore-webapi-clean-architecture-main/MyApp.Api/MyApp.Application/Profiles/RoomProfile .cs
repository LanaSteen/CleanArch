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
            CreateMap<RoomEntity, RoomDto>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));

            CreateMap<ReservationEntity, ReservationDto>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => $"{src.Guest.FirstName} {src.Guest.LastName}"));

            CreateMap<CreateRoomRequest, RoomEntity>();
            CreateMap<UpdateRoomRequest, RoomEntity>();
        }
    }
    

}
