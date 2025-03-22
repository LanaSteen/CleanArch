using MediatR;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands.Hotel
{
    public record DeleteHotelCommand(int HotelId) : IRequest<string>;

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, string>
    {
        private readonly IHotelRepository hotelRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public async Task<string> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var (isSuccess, message) = await hotelRepository.DeleteHotelAsync(request.HotelId);

            return message;
        }
    }
}
