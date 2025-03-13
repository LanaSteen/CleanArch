using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Queries
{
    public record GetAllHotelsQuery() : IRequest<IEnumerable<HotelEntity>>;

    public class GetAllHotelsQueryHandler(IHotelRepository hotelRepository)
        : IRequestHandler<GetAllHotelsQuery, IEnumerable<HotelEntity>>
    {
        public async Task<IEnumerable<HotelEntity>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            return await hotelRepository.GetHotels();
        }
    }
}
