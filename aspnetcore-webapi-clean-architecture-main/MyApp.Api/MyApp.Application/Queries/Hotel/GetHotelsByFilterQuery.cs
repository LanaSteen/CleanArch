using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Hotel;
using MyApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Hotel
{
    public record GetHotelsByFilterQuery(string? Country, string? City, int? MinRating, int? MaxRating) : IRequest<List<HotelDto>>;

    public class GetHotelsByFilterQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        : IRequestHandler<GetHotelsByFilterQuery, List<HotelDto>>
    {
        public async Task<List<HotelDto>> Handle(GetHotelsByFilterQuery request, CancellationToken cancellationToken)
        {
            var hotels = await hotelRepository.GetHotels();

            // Apply filtering
            if (!string.IsNullOrWhiteSpace(request.Country))
            {
                hotels = hotels.Where(h => h.Country.ToLower() == request.Country.ToLower()).ToList();
            }

            if (!string.IsNullOrWhiteSpace(request.City))
            {
                hotels = hotels.Where(h => h.City.ToLower() == request.City.ToLower()).ToList();
            }

            if (request.MinRating.HasValue)
            {
                hotels = hotels.Where(h => h.Rating >= request.MinRating.Value).ToList();
            }

            if (request.MaxRating.HasValue)
            {
                hotels = hotels.Where(h => h.Rating <= request.MaxRating.Value).ToList();
            }

            return hotels.Select(mapper.Map<HotelDto>).ToList();
        }
    }
}
