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
            CreateMap<ReservationEntity, ReservationDto>();
            CreateMap<CreateReservationRequest, ReservationEntity>();
            CreateMap<UpdateReservationRequest, ReservationEntity>();
        }
    }
}