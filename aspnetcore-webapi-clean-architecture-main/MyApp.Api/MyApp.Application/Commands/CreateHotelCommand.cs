using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands
{
    public record CreateHotelCommand(HotelEntity Hotel) : IRequest<HotelEntity>;

    public class CreateHotelCommandHandler(IHotelRepository hotelRepository)
        : IRequestHandler<CreateHotelCommand, HotelEntity>
    {
        public async Task<HotelEntity> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            return await hotelRepository.AddHotelAsync(request.Hotel);
        }
    }
}
