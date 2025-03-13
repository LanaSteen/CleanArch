using MediatR;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands
{
    public record DeleteHotelCommand(int HotelId) : IRequest<bool>;

    public class DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        : IRequestHandler<DeleteHotelCommand, bool>
    {
        public async Task<bool> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            return await hotelRepository.DeleteHotelAsync(request.HotelId);
        }
    }
}
