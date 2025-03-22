using AutoMapper;
using MyApp.Application.DTOs.Manager;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<CreateManagerRequest, ManagerEntity>();
            CreateMap<UpdateManagerRequest, ManagerEntity>();
            CreateMap<ManagerEntity, ManagerDto>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name));

        }
    }

}
