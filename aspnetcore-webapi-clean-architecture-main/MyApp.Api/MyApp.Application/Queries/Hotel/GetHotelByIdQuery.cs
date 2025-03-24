using AutoMapper;
using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Queries.Hotel
{

    public record GetHotelByIdQuery(int HotelId) : IRequest<HotelDto>;
    public class GetHotelByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
     : IRequestHandler<GetHotelByIdQuery, HotelDto>
    {
        public async Task<HotelDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId);
                if (hotel == null)
                {
                    throw new Exception($"Hotel with ID {request.HotelId} not found");
                }

                return mapper.Map<HotelDto>(hotel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetHotelByIdQueryHandler: {ex.Message}");
                throw;
            }
        }
    }
}
