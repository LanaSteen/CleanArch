using AutoMapper;
using MyApp.Application.DTOs.Guest;
using MyApp.Application.DTOs.Hotel;
using MyApp.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Application.Profiles
{
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            CreateMap<GuestEntity, GuestDto>();
            CreateMap<CreateGuestRequest, GuestEntity>();
            CreateMap<UpdateGuestRequest, GuestEntity>();
        }
    }
}