using AutoMapper;
using MyApp.Application.DTOs.Hotel;
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
            //CreateMap<HotelEntity, HotelDto>();

            //CreateMap<UpdateHotelRequest, HotelEntity>();

            // CreateMap<HotelEntity, HotelDto>()
            //.ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.ManagerId));
            CreateMap<CreateHotelRequest, HotelEntity>();

            CreateMap<HotelEntity, HotelDto>()
                .ForMember(dest => dest.ManagerId, opt =>
                    opt.MapFrom(src => src.Manager != null ? src.Manager.Id : (int?)null));

            CreateMap<UpdateHotelRequest, HotelEntity>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateHotelRequest, HotelEntity>()
             .ForMember(dest => dest.ManagerId, opt => opt.Ignore()); // არ შეიცვლება ავტომატურად


        }
    }
}