using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands
{
    public record UpdateHotelCommand(int HotelId, HotelEntity Hotel) : IRequest<HotelEntity>;

    public class UpdateHotelCommandHandler(IHotelRepository hotelRepository)
        : IRequestHandler<UpdateHotelCommand, HotelEntity>
    {
        public async Task<HotelEntity> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            return await hotelRepository.UpdateHotelAsync(request.HotelId, request.Hotel);
        }
    }
}
