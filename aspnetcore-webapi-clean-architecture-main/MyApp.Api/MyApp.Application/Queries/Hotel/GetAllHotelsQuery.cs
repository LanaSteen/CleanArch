using AutoMapper;
using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Queries.Hotel
{
    public record GetAllHotelsQuery() : IRequest<IEnumerable<HotelDto>>;

    public class GetAllHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, IEnumerable<HotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetAllHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _hotelRepository.GetHotels();
            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }
    }
}
