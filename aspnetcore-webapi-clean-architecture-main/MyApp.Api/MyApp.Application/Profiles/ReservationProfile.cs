using AutoMapper;
using MyApp.Application.DTOs.Guest;
using MyApp.Application.DTOs.Hotel;
using MyApp.Application.DTOs.Reservation;
using MyApp.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Application.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationEntity, ReservationDto>()
              .ForMember(dest => dest.GuestEmail, opt => opt.MapFrom(src => src.Guest.Email))
              .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
              .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name));

            CreateMap<CreateReservationRequest, ReservationEntity>();
            CreateMap<UpdateReservationRequest, ReservationEntity>();
        }
    }
}