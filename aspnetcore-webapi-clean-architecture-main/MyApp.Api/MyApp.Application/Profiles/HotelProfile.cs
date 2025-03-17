using AutoMapper;
using MyApp.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Application.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            //CreateMap<CreateHotelRequest, HotelEntity>();
            //CreateMap<HotelEntity, HotelDto>();
            //CreateMap<HotelEntity, CreateHotelRequest>().ReverseMap();
            CreateMap<HotelEntity, HotelDto>();
            CreateMap<CreateHotelRequest, HotelEntity>();
        }
    }
}