using AutoMapper;
using MyApp.Application.DTOs.Hotel;
using MyApp.Application.DTOs.Reservation;
using MyApp.Application.DTOs.Room;
using MyApp.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Application.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<CreateHotelRequest, HotelEntity>();

            CreateMap<HotelEntity, HotelDto>()
                .ForMember(dest => dest.ManagerId, opt =>
                    opt.MapFrom(src => src.Manager != null ? src.Manager.Id : (int?)null))
                .ForMember(dest => dest.ManagerName, opt =>
                    opt.MapFrom(src => src.Manager != null ? src.Manager.FirstName : null))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
                //.ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));

            CreateMap<UpdateHotelRequest, HotelEntity>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateHotelRequest, HotelEntity>().ForMember(dest => dest.ManagerId, opt => opt.Ignore());

            CreateMap<HotelDto, HotelEntity>()
                .ForMember(dest => dest.ManagerId, opt => opt.Condition(src => src.ManagerId.HasValue));
            CreateMap<CreateHotelRequest, HotelEntity>();

        }
    }
}


//public class HotelProfile : Profile
//{
//    public HotelProfile()
//    {
//        CreateMap<CreateHotelRequest, HotelEntity>();

//        CreateMap<HotelEntity, HotelDto>()
//            .ForMember(dest => dest.ManagerId, opt =>
//                opt.MapFrom(src => src.Manager != null ? src.Manager.Id : (int?)null))
//            .ForMember(dest => dest.ManagerName, opt =>
//                opt.MapFrom(src => src.Manager != null ? $"{src.Manager.FirstName} {src.Manager.LastName}" : null))
//            .ForMember(dest => dest.Rooms, opt => opt.Ignore())
//            .ForMember(dest => dest.Reservations, opt => opt.Ignore());

//        CreateMap<UpdateHotelRequest, HotelEntity>(MemberList.None)
//            .ForMember(dest => dest.ManagerId, opt => opt.Ignore())
//            .ForMember(dest => dest.Rooms, opt => opt.Ignore())
//            .ForMember(dest => dest.Reservations, opt => opt.Ignore())
//            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

//        CreateMap<HotelDto, HotelEntity>()
//            .ForMember(dest => dest.ManagerId, opt => opt.Condition(src => src.ManagerId.HasValue))
//            .ForMember(dest => dest.Rooms, opt => opt.Ignore())
//            .ForMember(dest => dest.Reservations, opt => opt.Ignore());
//    }
//}