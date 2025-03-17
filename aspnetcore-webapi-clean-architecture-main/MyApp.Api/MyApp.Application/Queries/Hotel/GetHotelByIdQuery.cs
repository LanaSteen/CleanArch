using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Queries.Hotel
{
    public record GetHotelByIdQuery(int HotelId) : IRequest<HotelEntity>;

    public class GetHotelByIdQueryHandler(IHotelRepository hotelRepository)
        : IRequestHandler<GetHotelByIdQuery, HotelEntity>
    {
        public async Task<HotelEntity> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            return await hotelRepository.GetHotelByIdAsync(request.HotelId);
        }
    }
}
